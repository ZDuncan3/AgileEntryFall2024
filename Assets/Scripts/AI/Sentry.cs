using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Sentry : MonoBehaviour
{
	[HideInInspector] public NPC npcInfo;

	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public GameObject target;

	private void Awake()
	{
		npcInfo = GetComponent<NPC>();
		agent = GetComponent<NavMeshAgent>();
	}

	private void OnTriggerStay(Collider collider)
	{
		GameObject _newTarget = collider.gameObject.GetComponent<PlayerController>()?.gameObject;

		Damageable _newTargetCharacter;

		if (_newTarget != null && target != null)
		{
			_newTargetCharacter = _newTarget.GetComponent<Player>();

			if (_newTargetCharacter.canBeTargeted)
			{
				if (Vector3.Distance(transform.position, _newTarget.transform.position) < Vector3.Distance(transform.position, target.transform.position))
				{
					target = _newTarget;
				}
			}
		}
		else
		{
			if (target == null && _newTarget != null)
			{
				_newTargetCharacter = _newTarget.GetComponent<Player>();

				if (_newTargetCharacter.canBeTargeted)
				{
					target = _newTarget;
				}
			}
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		target = null;
	}

	private void Start()
	{
		StartCoroutine(UpdateDestination());
	}

	private void Update()
	{
		if (target != null)
		{
			Vector3 direction = (target.transform.position - this.transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			lookRotation.x = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

			if (agent.remainingDistance <= agent.stoppingDistance + 1f)
			{
				if (npcInfo.animator != null)
				{
					if (!npcInfo.animator.GetBool("isAttacking"))
					{
						npcInfo.Attack();
					}
				}
			}
		}
	}

	private IEnumerator UpdateDestination()
	{
		while (true)
		{
			if (target != null)
			{
				agent.destination = target.transform.position;
			}
			else
			{
				agent.destination = this.transform.position;
			}
			yield return new WaitForSeconds(0.15f);
		}
	}
}