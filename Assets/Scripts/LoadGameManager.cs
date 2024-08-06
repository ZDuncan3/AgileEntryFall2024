using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public static LoadGameManager instance;

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

	public void LoadData()
	{
		GameData data = SaveLoad.Load("zduncan3AgileEntry");

		if (data != null)
		{
			if (data.isInSpirtDim == 1)
			{
				GameLogic.instance.isInSpiritDimension = true;
			}
			else
			{
				GameLogic.instance.isInSpiritDimension = false;
			}

			GameLogic.instance.hasDimSwitched = true;

			if (data.GetKeys.Count > 0)
			{
				for (int i = 0; i < data.GetKeys.Count; i++)
				{
					if (TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]) != null)
					{
						Rigidbody rb = TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]).GetComponent<Rigidbody>();
						PlayerController player = TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]).GetComponent<PlayerController>();

						if (player == null)
						{
							if (TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]) != null)
							{
								if (rb != null)
								{
									rb.position = data.GetValues[i].GetVector;
								}
								else
								{
									TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]).position = data.GetValues[i].GetVector;
								}

								i++;

								if (i <= data.GetKeys.Count)
								{
									if (TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]) != null)
									{
										// do the same for RB
										if (rb != null)
										{
											rb.rotation = data.GetValues[i].GetQuaternion;
										}
										else
										{
											TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]).rotation = data.GetValues[i].GetQuaternion;
										}
									}
								}
							}
							else
							{
								Debug.LogError($"Null Transform Reference Of Name: {TransformRegister.instance.GetObjectFromKey(data.GetKeys[i]).name}");
							}
						}
						else
						{
							rb.position = data.lastCheckpointPosition.GetVector;
							rb.rotation = data.lastCheckpointRotation.GetQuaternion;
						}
					}
				}
			}
		}
	}
}