using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
	private TMP_Text fpsText;
	private float deltaTime;

	private void Awake()
	{
		fpsText = GetComponent<TMP_Text>();
	}

	private void Update()
	{
		if (GameLogic.instance.showFps)
		{
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
			float fps = 1.0f / deltaTime;
			fpsText.text = Mathf.Ceil(fps).ToString();
		}
	}
}
