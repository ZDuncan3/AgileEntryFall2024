using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [Range(1, 32767)]public int cameraNum = 1; // based on which camera you want, set to correct item in list by CameraController (1st camera, 2nd camera, etc...)

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			CameraController.instance.ChangeCamera(cameraNum);
		}
	}
}