using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ChangeDimension : MonoBehaviour
{
	[Header("Dimension Settings")]
	[SerializeField] private bool changeTo = false;
	[SerializeField] private bool toggleInstead = false;
	[SerializeField] private bool disableSelf = false;

	[Header("Camera Settings")]
	[SerializeField] private bool shouldChangeCamera = false;
	[Range(1, 32767), SerializeField] private int cameraNum = 1;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			if (toggleInstead)
			{
				GameLogic.instance.isInSpiritDimension = !GameLogic.instance.isInSpiritDimension;
			}
            else
            {
				GameLogic.instance.isInSpiritDimension = changeTo;
			}

			GameLogic.instance.hasDimSwitched = true;

			if (shouldChangeCamera)
			{
				CameraController.instance.ChangeCamera(cameraNum);
			}

			if (disableSelf)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
