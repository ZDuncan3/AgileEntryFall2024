using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public bool needUnlockOne = true;
    public bool unlockOne = false;
	public string keyOneName;
    public bool needUnlockTwo = false;
    public bool unlockTwo = false;
	public string keyTwoName;

	public List<Door> doors = new List<Door>();

    public MeshRenderer[] runes;

    public Material filledMat;
    public Material blankMat;

	private void Start()
	{
		DoorManager.instance.doors.Add(this);
	}

	private void FixedUpdate()
	{
		if (needUnlockOne && unlockOne)
        {
			UpdateMaterials(runes[0], filledMat);
			UpdateMaterials(runes[1], filledMat);

			if (!needUnlockTwo)
			{
				foreach (var door in  doors)
				{
					if (!door.isOpen)
					{
						door.OpenDoor();
						door.isOpen = true;
					}
				}
			}
		}
        else if (needUnlockOne)
        {
			UpdateMaterials(runes[0], blankMat);
			UpdateMaterials(runes[1], blankMat);
		}

        if (needUnlockTwo && unlockTwo)
        {
			UpdateMaterials(runes[2], filledMat);
			UpdateMaterials(runes[3], filledMat);

			if (needUnlockOne && unlockOne)
			{
				foreach(var door in doors)
				{
					if (!door.isOpen)
					{
						door.OpenDoor();
						door.isOpen = true;
					}
				}
			}
		}
		else if (needUnlockTwo)
		{
			UpdateMaterials(runes[2], blankMat);
			UpdateMaterials(runes[3], blankMat);
		}
	}

	private void UpdateMaterials(MeshRenderer renderer, Material material)
	{
		Material[] mats = renderer.materials;

		mats[0] = renderer.materials[0];
		mats[1] = material;

		renderer.materials = mats;
	}
}
