using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
	public static RespawnController instance;

	public float bottomBounds = -20f;

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

	private void FixedUpdate()
	{
		if (PlayerController.instance.transform.position.y < bottomBounds)
		{
			CheckpointManager.instance.ResetPlayerToCheckpoint();
		}
	}
}
