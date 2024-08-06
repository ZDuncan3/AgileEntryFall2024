using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
public class GUIDObject : MonoBehaviour
{
    [SerializeField] private string _ID; // remove serializeField

	private void OnEnable()
	{
		if (_ID == string.Empty)
		{
			GenerateGUID();
		}
	}

	private void Start()
	{
		TransformRegister.instance?.RegisterObject(_ID, transform);
	}

	public void GenerateGUID()
	{
		_ID = Guid.NewGuid().ToString();
	}
}