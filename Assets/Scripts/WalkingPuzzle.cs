using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPuzzle : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			CheckpointManager.instance.ResetPlayerToCheckpoint();

			//transform.position = new Vector3(transform.position.x, transform.position.y - 20, transform.position.z);
		}
	}
}
