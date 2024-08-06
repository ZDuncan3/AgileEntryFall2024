using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [HideInInspector] public Vector3 spawnPosition = Vector3.zero;
	[HideInInspector] public Quaternion spawnRotation = Quaternion.identity;

	private void Awake()
	{
		spawnPosition = transform.position;
		spawnRotation = transform.rotation;

		if (GetComponent<MeshRenderer>() != null)
		{
			GetComponent<MeshRenderer>().enabled = false;
			transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			CheckpointManager.instance.currentCheckpoint = this;
		}
	}

	public Checkpoint(Vector3 position, Quaternion rotation)
	{
		spawnPosition = position;
		spawnRotation = rotation;
	}
}
