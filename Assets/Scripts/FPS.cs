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
			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
			float fps = 1.0f / deltaTime;

			if (fps < 0)
			{
				fps = 0;
			}
			else if (fps > Application.targetFrameRate)
			{
				fps = Application.targetFrameRate;
			}

			fpsText.text = Mathf.Ceil(fps).ToString();
		}
		else
		{
			fpsText.text = "";
		}
	}
}
