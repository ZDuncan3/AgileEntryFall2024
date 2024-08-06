using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRegister : MonoBehaviour
{
	public static TransformRegister instance;

	private Dictionary<string, Transform> _objectTransforms = new Dictionary<string, Transform>();

	public Dictionary<string, Transform> GetObjectTransforms {  get { return _objectTransforms; } }

	private void Awake()
	{
		instance = this;
	}

	public void RegisterObject(string key, Transform value)
	{
		if (!_objectTransforms.ContainsKey(key))
		{
			_objectTransforms.Add(key, value);
		}
	}

	public Transform GetObjectFromKey(string key)
	{
		if (_objectTransforms.ContainsKey(key))
		{
			return _objectTransforms[key];
		}

		return null;
	}
}