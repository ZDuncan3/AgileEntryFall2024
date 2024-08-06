using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

	public List<Checkpoint> checkpoints = new List<Checkpoint>();

	public Checkpoint currentCheckpoint;

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
		if (currentCheckpoint == null && checkpoints.Count > 0)
		{
			currentCheckpoint = checkpoints[0];
		}
		else
		{
			Debug.LogError("Must have at least one checkpoint in the checkpoints list!");
		}
	}

	public void ResetPlayerToCheckpoint()
	{
		PlayerController.instance.ResetVelocity();
		PlayerController.instance.SetPlayerPosition(currentCheckpoint.spawnPosition);
		PlayerController.instance.SetPlayerRotation(currentCheckpoint.spawnRotation);
	}
}
