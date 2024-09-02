using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockDoor : MonoBehaviour
{
    public DoorLock doorToUnlock;

	public bool keyOneInserted = false;
	public GameObject keyOneStatic;
	public GameObject keyOne;
	public bool keyTwoInserted = false;
	public GameObject keyTwoStatic;
	public GameObject keyTwo;

	private void Awake()
	{
		keyOneStatic.SetActive(false);
		keyTwoStatic.SetActive(false);
	}

	private void Start()
	{
		DoorManager.instance.unlocks.Add(this);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (PlayerController.instance.keys.Count > 0)
		{
			for (int i = 0; i < PlayerController.instance.keys.Count; i++)
			{
				if (PlayerController.instance.keys[i].Equals(doorToUnlock.keyOneName))
				{
					keyOneInserted = true;
					doorToUnlock.unlockOne = true;
					keyOneStatic.gameObject.SetActive(true);
					PlayerController.instance.keys.RemoveAt(i);
					i--;
					if (PlayerController.instance.keys.Count <= 0)
					{
						break;
					}
				}

				if (doorToUnlock.needUnlockTwo && !doorToUnlock.unlockTwo)
				{
					if (PlayerController.instance.keys[i].Equals(doorToUnlock.keyTwoName))
					{
						keyTwoInserted = true;
						doorToUnlock.unlockTwo = true;
						keyTwoStatic.gameObject.SetActive(true);
						PlayerController.instance.keys.RemoveAt(i);
						i--;
						if (PlayerController.instance.keys.Count <= 0)
						{
							break;
						}
					}
				}
			}
		}
	}
}
