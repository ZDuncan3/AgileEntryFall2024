using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;

    public List<DoorLock> doors = new List<DoorLock>();
	public List<UnlockDoor> unlocks = new List<UnlockDoor>();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}
}
