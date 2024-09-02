using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private List<string> _keys = new List<string>();
    private List<VectorToken> _values = new List<VectorToken>();

    public List<string> GetKeys { get { return _keys; } }
    public List<VectorToken> GetValues { get { return _values; } }

    public VectorToken lastCheckpointPosition = new VectorToken(Vector3.zero);
    public VectorToken lastCheckpointRotation = new VectorToken(Quaternion.identity);

    [Range(0,1), HideInInspector] public int isInSpirtDim = 0; // 0 = not in spirit dim, 1 = in spirit dim

	[Range(0, 1), HideInInspector] public int hasKeyOne = 0;
	[Range(0, 1), HideInInspector] public int hasKeyTwo = 0;

	[Range(0, 1), HideInInspector] public int unlockOne = 0;
	[Range(0, 1), HideInInspector] public int unlockTwo = 0;

	[Range(0, 1), HideInInspector] public int keyOneInserted = 0;
	[Range(0, 1), HideInInspector] public int keyTwoInserted = 0;

	public GameData()
    {
        foreach (KeyValuePair<string, Transform> item in TransformRegister.instance.GetObjectTransforms)
        {
            _keys.Add(item.Key);
            _values.Add(new VectorToken(item.Value.position));
            _keys.Add(item.Key);
            _values.Add(new VectorToken(item.Value.rotation));
        }

        lastCheckpointPosition = new VectorToken(CheckpointManager.instance.currentCheckpoint.spawnPosition);
        lastCheckpointRotation = new VectorToken(CheckpointManager.instance.currentCheckpoint.spawnRotation);

        if (GameLogic.instance.isInSpiritDimension)
        {
            isInSpirtDim = 1;
        }
        else
        {
            isInSpirtDim = 0;
        }

        if (DoorManager.instance.doors[0].unlockOne)
        {
            unlockOne = 1;
        }
        else
        {
            unlockOne = 0;
        }

		if (DoorManager.instance.doors[0].unlockTwo)
		{
			unlockTwo = 1;
		}
		else
		{
			unlockTwo = 0;
		}

        if (PlayerController.instance.keys.Count > 1)
        {
			if (PlayerController.instance.keys[0].Equals("Key 1") || PlayerController.instance.keys[1].Equals("Key 1"))
			{
				hasKeyOne = 1;
			}
			else
			{
				hasKeyOne = 0;
			}

			if (PlayerController.instance.keys[0].Equals("Key 2") || PlayerController.instance.keys[1].Equals("Key 2"))
			{
				hasKeyTwo = 1;
			}
			else
			{
				hasKeyTwo = 0;
			}
		}
        else if (PlayerController.instance.keys.Count > 0)
        {
			if (PlayerController.instance.keys[0].Equals("Key 1"))
			{
				hasKeyOne = 1;
			}
			else
			{
				hasKeyOne = 0;
			}

			if (PlayerController.instance.keys[0].Equals("Key 2"))
			{
				hasKeyTwo = 1;
			}
			else
			{
				hasKeyTwo = 0;
			}
		}
        else
        {
            hasKeyOne = 0;
            hasKeyTwo = 0;
        }

        if (DoorManager.instance.unlocks[0].keyOneInserted)
        {
            keyOneInserted = 1;
        }
        else
        {
            keyOneInserted = 0;
        }

		if (DoorManager.instance.unlocks[0].keyTwoInserted)
		{
			keyTwoInserted = 1;
		}
		else
		{
			keyTwoInserted = 0;
		}
	}
}

[System.Serializable]
public class VectorToken
{
    private float _x;
    private float _y;
    private float _z;
    private float _w;

    public Vector3 GetVector { get { return new Vector3(_x, _y, _z); } }
    public Quaternion GetQuaternion { get { return new Quaternion(_x, _y, _z, _w); } }

    public VectorToken(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public VectorToken(Vector3 vector)
    {
        _x = vector.x;
        _y = vector.y;
        _z = vector.z;
    }

    public VectorToken(float x, float y, float z, float w)
    {
        _x = x;
        _y = y;
        _z = z;
        _w = w;
    }

    public VectorToken(Quaternion quaternion)
    {
        _x = quaternion.x;
        _y = quaternion.y;
        _z = quaternion.z;
        _w = quaternion.w;
    }
}