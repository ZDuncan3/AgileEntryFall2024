using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
	public float rotateSpeed = 20f;

	public bool rotateAroundX = true;
	public bool rotateAroundY = false;
	public bool rotateAroundZ = false;

	private void Update()
	{
		if (rotateAroundX)
			transform.Rotate(Vector3.left, rotateSpeed * Time.deltaTime);
		else if (rotateAroundY)
			transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
		else if (rotateAroundZ)
			transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
	}
}
