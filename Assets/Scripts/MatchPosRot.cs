using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPosRot : MonoBehaviour
{
	[Header("Objects To Match")]
	public GameObject objectToRotateWith;
	public GameObject objectToMoveWith;

	[Header("Match Rotation")]
	public bool rotX;
	public bool rotY;
	public bool rotZ;
	public Quaternion rotOffset;

	[Header("Match Position")]
	public bool posX;
	public bool posY;
	public bool posZ;
	public Vector3 posOffset;

	void LateUpdate()
	{
		// check every possible bool combination and apply rotations accordingly
		// not big brain, but works
		if (objectToRotateWith != null)
		{
			if (rotX && rotY && rotZ) // x, y, z
				transform.rotation = new Quaternion(objectToRotateWith.transform.rotation.x + rotOffset.x, objectToRotateWith.transform.rotation.y + rotOffset.y, objectToRotateWith.transform.rotation.z + rotOffset.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
			else if (rotX && rotY && !rotZ) // x, y
				transform.rotation = new Quaternion(objectToRotateWith.transform.rotation.x + rotOffset.x, objectToRotateWith.transform.rotation.y + rotOffset.y, transform.rotation.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
			else if (rotX && !rotY && rotZ) // x, z
				transform.rotation = new Quaternion(objectToRotateWith.transform.rotation.x + rotOffset.x, transform.rotation.y, objectToRotateWith.transform.rotation.z + rotOffset.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
			else if (rotX && !rotY && !rotZ) // x
				transform.rotation = new Quaternion(objectToRotateWith.transform.rotation.x + rotOffset.x, transform.rotation.y, transform.rotation.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
			else if (!rotX && rotY && rotZ) // y, z
				transform.rotation = new Quaternion(transform.rotation.x, objectToRotateWith.transform.rotation.y + rotOffset.y, objectToRotateWith.transform.rotation.z + rotOffset.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
			else if (!rotX && rotY && !rotZ) // y
				transform.rotation = new Quaternion(transform.rotation.x, objectToRotateWith.transform.rotation.y + rotOffset.y, transform.rotation.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
			else if (!rotX && !rotY && rotZ) // z
				transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, objectToRotateWith.transform.rotation.z + rotOffset.z, objectToRotateWith.transform.rotation.w + rotOffset.w);
		}

		// check every possible bool combination and apply position accordingly
		// not big brain, but works
		if (objectToMoveWith != null)
		{
			if (posX && posY && posZ) // x, y, z
				transform.position = new Vector3(objectToMoveWith.transform.position.x + posOffset.x, objectToMoveWith.transform.position.y + posOffset.y, objectToMoveWith.transform.position.z + posOffset.z);
			else if (posX && posY && !posZ) // x, y
				transform.position = new Vector3(objectToMoveWith.transform.position.x + posOffset.x, objectToMoveWith.transform.position.y + posOffset.y, transform.position.z);
			else if (posX && !posY && posZ) // x, z
				transform.position = new Vector3(objectToMoveWith.transform.position.x + posOffset.x, transform.position.y, objectToMoveWith.transform.position.z + posOffset.z);
			else if (posX && !posY && !posZ) // x
				transform.position = new Vector3(objectToMoveWith.transform.position.x + posOffset.x, transform.position.y, transform.position.z);
			else if (!posX && posY && posZ) // y, z
				transform.position = new Vector3(transform.position.x, objectToMoveWith.transform.position.y + posOffset.y, objectToMoveWith.transform.position.z + posOffset.z);
			else if (!posX && posY && !posZ) // y
				transform.position = new Vector3(transform.position.x, gameObject.transform.position.y + posOffset.y, transform.position.z);
			else if (!posX && !posY && posZ) // z
				transform.position = new Vector3(transform.position.x, transform.position.y, objectToMoveWith.transform.position.z + posOffset.z);
		}
	}
}
