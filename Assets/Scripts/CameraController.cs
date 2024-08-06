using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController instance;

    public List<Transform> cameras = new List<Transform>();

	public int currentCamera;

	public GameObject spiritDimensionPostProcess;

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
	}

	private void Start()
	{
		PlayerController.instance.camController = this;
	}

	private void FixedUpdate()
	{
		if (GameLogic.instance.isInSpiritDimension)
		{
			if (!spiritDimensionPostProcess.activeInHierarchy)
				spiritDimensionPostProcess.SetActive(true);
		}
		else if (!GameLogic.instance.isInSpiritDimension)
		{
			if (spiritDimensionPostProcess.activeInHierarchy)
				spiritDimensionPostProcess.SetActive(false);
		}
	}

	private void LateUpdate()
	{
		if (GameLogic.instance.isInSpiritDimension)
		{
			spiritDimensionPostProcess.transform.position = cameras[currentCamera].transform.position;
		}
	}

	public void ChangeCamera()
	{
		if (currentCamera + 1 >= cameras.Count)
		{
			cameras[currentCamera].parent.gameObject.SetActive(false);
			currentCamera = 0;
			cameras[currentCamera].parent.gameObject.SetActive(true);
		}
		else
		{
			cameras[currentCamera].parent.gameObject.SetActive(false);
			currentCamera++;
			cameras[currentCamera].parent.gameObject.SetActive(true);
		}

		PlayerController.instance.cameraChangeLock = true;
	}

	public void ChangeCamera(int cameraNum)
	{
		cameraNum--;// reduce number by 1 to set the incoming value correctly for the camera list (the 1st camera is the 0th in the list)
		cameras[currentCamera].parent.gameObject.SetActive(false);
		currentCamera = cameraNum;
		cameras[currentCamera].parent.gameObject.SetActive(true);

		PlayerController.instance.cameraChangeLock = true;
	}
}