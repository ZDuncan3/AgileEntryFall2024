using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	//[Header("References")]
	public static PlayerController instance;

	[Header("Controller Stats")]
    [SerializeField] private float _dashPower = 10f;
	public float _playerHeight = 2f; // used to check for grounded, default value = 2f
	private float _moveSpeed;
	[SerializeField] private float _walkSpeed = 2.5f; // slow speed (left alt), default value = 1.5f
	[SerializeField] private float _runSpeed = 4f; // normal speed, default value = 4.0f
	[SerializeField] private float _sprintSpeed = 5.5f; // fast speed (left shift), default value = 8.0f
	[SerializeField] private float _groundDrag = 5f; // drag on the ground, default value = 5.0f
	[SerializeField] private float _airDragReduction = 2.5f; // reduce drag by this amount while in the air (increases speed/acceleration), default value = 2.5f (half of _groundDrag)
	[SerializeField] private float _jumpPower = 6.5f; // force of jump, default value = 6.5f
	[SerializeField] private float _dashTimer = 2f;

	public bool enablePlayerControls = false; // can the player be controlled

	[HideInInspector] public CameraController camController;

	private Rigidbody _rb;

	private Transform _camOrientation;

	private Vector3 _moveDirection;

	[HideInInspector] public bool cameraChangeLock = false;

	[Header("Keyboard Controls")]
	public InputActionAsset playerControls;
	[HideInInspector] public InputAction moveKeys;// default = W>A>S>D respectively
	[HideInInspector] public InputAction sprintKey;// default = Left Shift
	[HideInInspector] public InputAction walkKey;// default = Left Alt
	[HideInInspector] public InputAction jumpKey;// default = Space
	[HideInInspector] public InputAction dashKey;// default = X
	[HideInInspector] public InputAction primaryAttackButton;// default = LMB

	[Header("Misc")]
	public LayerMask groundLayer; // layer to check for isGrounded, MANUAL set
	[SerializeField] private float _maxSlopeAngle = 50f; // prevents the controller from being able to climb too steep of an angle, default = 50f
	private RaycastHit _slopeHit; // used to check the slope angle, automatically set
	private bool _isMoving = false; // automatically set
	private bool _isJumping = false; // automatically set
	private bool _isDashing = false; // automatically set
	private bool _isGrounded = true; // automatically set
	private bool _isFalling = false;
	private bool _isLightAttacking = false; // automatically set
	private bool _isHeavyAttacking = false; // automatically set
	private bool _jumpReady = true; // automatically set
	private bool _dashReady = false; // automatically set
	private bool _dashInProgress = false; // automatically set
	private Animator _animator;
	[SerializeField] private PhysicMaterial slickMat;
	private Collider _playerCollider;

	public List<string> keys = new List<string>();

	private float timeFalling = 0f;
	private float timeAttacking = 0f;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}

		_rb = GetComponent<Rigidbody>();

		_camOrientation = Camera.main.GetComponent<Transform>();

		_dashReady = true;

		_animator = GetComponent<Animator>();

		_playerCollider = GetComponent<Collider>();
	}

	private void Start()
	{
		LoadGameManager.instance.LoadData();
	}

	private void OnEnable()
	{
		moveKeys = playerControls.FindAction("Move");
		moveKeys.Enable();
		sprintKey = playerControls.FindAction("Sprint");
		sprintKey.Enable();
		walkKey = playerControls.FindAction("Walk");
		walkKey.Enable();
		jumpKey = playerControls.FindAction("Jump");
		jumpKey.Enable();
		dashKey = playerControls.FindAction("Dash");
		dashKey.Enable();
		primaryAttackButton = playerControls.FindAction("Attack");
		primaryAttackButton.Enable();
	}

	private void OnDisable()
	{
		moveKeys.Disable();
		sprintKey.Disable();
		walkKey.Disable();
		jumpKey.Disable();
		dashKey.Disable();
		primaryAttackButton.Disable();
	}

	private void Update()
	{
		if (!cameraChangeLock)
			_camOrientation = camController.cameras[camController.currentCamera];

		HandleMenuInput();
		HandleMovementInput();
		HandleAttackInput();
		HandleSaveLoadInput();

		if (_isGrounded)
		{
			_rb.drag = _groundDrag;
			_playerCollider.material = null;
		}
		else
		{
			_rb.drag = 0;
			_playerCollider.material = slickMat;
		}
	}

	private void FixedUpdate()
	{
		HandleMovement();
	}

	private void HandleMenuInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameLogic.instance.isMenuOpen)
			{
				Time.timeScale = 1.0f;
				GameLogic.instance.isMenuOpen = false;

				foreach (var panel in Buttons.instance.menuPanels)
				{
					panel.gameObject.SetActive(false);
				}
			}
			else
			{
				if (Buttons.instance.lastOpenedMenu != null)
					Buttons.instance.lastOpenedMenu.gameObject.SetActive(true);
				else
					Buttons.instance.menuPanels[0].gameObject.SetActive(true);
				GameLogic.instance.isMenuOpen = true;
				Time.timeScale = 0f;
			}
		}
	}

	private void HandleSaveLoadInput()
	{
		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			SaveLoad.Save("zduncan3AgileEntry");
		}

		if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			LoadGameManager.instance.LoadData();
		}
	}

	private void HandleAttackInput()
	{
		if (primaryAttackButton.WasPressedThisFrame())
		{
			timeAttacking = 0f;
		}
		else if (primaryAttackButton.WasReleasedThisFrame())
		{
			if (timeAttacking >= 0.2f)
			{
				_isLightAttacking = false;
				_isHeavyAttacking = true;
			}
			else
			{
				_isLightAttacking = true;
				_isHeavyAttacking = false;
			}

			timeAttacking = 0f;
		}
		else if (primaryAttackButton.IsPressed())
		{
			timeAttacking += Time.deltaTime;

			if (timeAttacking >= 0.2f)
			{
				_isLightAttacking = false;
				_isHeavyAttacking = true;
			}
		}
		else
		{
			_isLightAttacking = false;
			_isHeavyAttacking = false;
		}
		_animator.SetBool("isLightAttacking", _isLightAttacking);
		_animator.SetBool("isHeavyAttacking", _isHeavyAttacking);
	}

	private void HandleMovement()// done in FixedUpdate
	{
		// falling check, can add layers to check if needed
		_isFalling = !Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, groundLayer);

		if (_isFalling)
		{
			timeFalling += Time.deltaTime;

			if (timeFalling > 0.15f && _isGrounded)
			{
				_isGrounded = false;
			}
			else if (timeFalling > 10.0f)
			{
				_isFalling = false;
				_isGrounded = true;
				timeFalling = 0f;
			}
		}
		else
		{
			_isGrounded = true;
			timeFalling = 0f;
		}

		if (_isGrounded)
		{
			_jumpReady = true;
			_animator.SetBool("isJumping", false);
		}
		_animator.SetBool("isGrounded", _isGrounded);

		if (OnSlope())
		{
			_rb.AddForce(GetSlopeMoveDirection() * _moveSpeed * 10f, ForceMode.Force);
		}
		else if (_isGrounded)
		{
			_rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
		}
		else if (!_isGrounded)
		{
			// remove this line to prevent the player from being able to change direction while in air
			_rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _airDragReduction, ForceMode.Force);
		}

		// perform jump
		if (_jumpReady && _isJumping)
		{
			_rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
			_rb.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);

			_jumpReady = false;
			_animator.SetBool("isJumping", true);
			StartCoroutine(AnimatorJumpReset());
			//_isJumping = false;
		}

		// perform dash
		if (_dashReady && _isDashing)
		{
			_rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
			_rb.useGravity = false;

			if (_moveDirection.magnitude != 0)
			{
				_rb.AddForce(_moveDirection * _dashPower, ForceMode.Impulse);
			}
			else
			{
				_rb.AddForce(transform.forward * _dashPower * 10f, ForceMode.Impulse);
			}
			_dashReady = false;
			_dashInProgress = true;
			StartCoroutine(DashTime());
			StartCoroutine(DashCooldown());
		}

		// rotate player
		if (_moveDirection.magnitude != 0f)
		{
			Quaternion moveRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, moveRotation, 600f * Time.deltaTime);
		}

		// Speed limits
		if (_dashInProgress)
		{
			Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

			if (flatVel.magnitude > _moveSpeed * 2.5f)
			{
				Vector3 limitedVel = flatVel.normalized * _moveSpeed * 2.5f;
				_rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
			}
		}
		else if (OnSlope())
		{
			if (_rb.velocity.magnitude > _moveSpeed)
				_rb.velocity = _rb.velocity.normalized * _moveSpeed;
		}
		else
		{
			Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

			if (flatVel.magnitude > _moveSpeed)
			{
				Vector3 limitedVel = flatVel.normalized * _moveSpeed;
				_rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
			}
		}
	}

	private IEnumerator AnimatorJumpReset()
	{
		yield return new WaitForSeconds(0.3f);
		_animator.SetBool("isJumping", false);
	}

	private void HandleMovementInput()// done in update
	{
		// track if we are moving
		if (moveKeys.ReadValue<Vector2>().magnitude != 0)
		{
			_isMoving = true;
		}
		else
		{
			_isMoving = false;
		}

		// remove the camera lock so that our movement is tied to the new camera transform
		if (!_isMoving && cameraChangeLock == true)
		{
			cameraChangeLock = false;
		}

		// jump
		if (jumpKey.IsPressed() && _jumpReady)
		{
			_isJumping = true;
		}
		else
		{
			_isJumping = false;
		}

		// dash
		if (dashKey.IsPressed())
		{
			_isDashing = true;
		}
		else
		{
			_isDashing = false;
		}

		// determine controller moveSpeed
		if (sprintKey.IsPressed())
		{
			_moveSpeed = _sprintSpeed;
			_animator.SetBool("isWalking", false);
			_animator.SetBool("isSprinting", true);
		}
		else if (walkKey.IsPressed())
		{
			_moveSpeed = _walkSpeed;
			_animator.SetBool("isSprinting", false);
			_animator.SetBool("isWalking", true);
		}
		else
		{
			_moveSpeed = _runSpeed;
			_animator.SetBool("isSprinting", false);
			_animator.SetBool("isWalking", false);
		}

		_animator.SetBool("isMoving", _isMoving);

		// normal movement (new input system)
		if (moveKeys.ReadValue<Vector2>().y > 0) // moving forward
		{
			if (moveKeys.ReadValue<Vector2>().x < 0) // moving forward and left
			{
				_moveDirection = _camOrientation.forward - _camOrientation.right;
			}
			else if (moveKeys.ReadValue<Vector2>().x > 0) // moving forward right
			{
				_moveDirection = _camOrientation.forward + _camOrientation.right;
			}
			else
			{
				_moveDirection = _camOrientation.forward;
			}
		}
		else if (moveKeys.ReadValue<Vector2>().y < 0) // moving backward
		{
			if (moveKeys.ReadValue<Vector2>().x < 0) // moving backward and left
			{
				_moveDirection = -_camOrientation.forward - _camOrientation.right;
			}
			else if (moveKeys.ReadValue<Vector2>().x > 0) // moving backward and right
			{
				_moveDirection = -_camOrientation.forward + _camOrientation.right;
			}
			else
			{
				_moveDirection = -_camOrientation.forward;
			}
		}
		else if (moveKeys.ReadValue<Vector2>().x < 0) // moving left
		{
			_moveDirection = -_camOrientation.right;
		}
		else if (moveKeys.ReadValue<Vector2>().x > 0) // moving right
		{
			_moveDirection = _camOrientation.right;
		}
		else
		{
			_moveDirection = _camOrientation.forward * 0 + _camOrientation.right * 0;
		}
	}

	private bool OnSlope()
	{
		if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.2f))
		{
			float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
			return angle < _maxSlopeAngle && angle != 0;
		}

		return false;
	}

	private Vector3 GetSlopeMoveDirection()
	{
		return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
	}

	private IEnumerator DashCooldown()
	{
		yield return new WaitForSeconds(_dashTimer);
		_dashReady = true;
	}

	private IEnumerator DashTime()
	{
		yield return new WaitForSeconds(0.2f);
		_rb.useGravity = true;
		_dashInProgress = false;
	}

	public void SetPlayerPosition(Vector3 position)
	{
		_rb.position = position;
	}

	public void SetPlayerRotation(Quaternion rotation)
	{
		_rb.rotation = rotation;
	}

	public void ResetVelocity()
	{
		_rb.velocity = Vector3.zero;
	}
}