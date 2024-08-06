using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GUIDObject))]
public class GUIDObjectEditor : Editor
{
	private GUIDObject _target;

	private void OnEnable()
	{
		_target = (GUIDObject)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Generate New GUID"))
		{
			_target.GenerateGUID();
		}
	}
}
#endif