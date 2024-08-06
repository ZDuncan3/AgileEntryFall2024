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