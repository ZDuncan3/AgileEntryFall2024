using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;

	public float volume;
	public int maxFrameRate = 60;

	public bool isMenuOpen = false;
	public bool showFps = true;
	public bool isInSpiritDimension = false; // is the player in the spirit dimension

	public List<GameObject> objsEnabledInSpiritDim = new List<GameObject>();
	public List<GameObject> objsDisabledInSpiritDim = new List<GameObject>();

	public bool hasDimSwitched = false;

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

		Application.targetFrameRate = maxFrameRate;

		DontDestroyOnLoad(gameObject);

		//StartCoroutine(CheckDim());
	}

	private void FixedUpdate()
	{
		if (hasDimSwitched)
		{
			if (isInSpiritDimension)
			{
				foreach (var obj in objsDisabledInSpiritDim)
				{
					obj.SetActive(false);
				}
				foreach (var obj in objsEnabledInSpiritDim)
				{
					obj.SetActive(true);
				}
			}
			else
			{
				foreach (var obj in objsDisabledInSpiritDim)
				{
					obj.SetActive(true);
				}
				foreach (var obj in objsEnabledInSpiritDim)
				{
					obj.SetActive(false);
				}
			}

			hasDimSwitched = false;
		}
	}

	private IEnumerator CheckDim()
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(0.075f);

			if (hasDimSwitched)
			{
				if (isInSpiritDimension)
				{
					foreach (GameObject obj in objsDisabledInSpiritDim)
					{
						obj.SetActive(false);
					}
					foreach (GameObject obj in objsEnabledInSpiritDim)
					{
						obj.SetActive(true);
					}
				}
				else
				{
					foreach (GameObject obj in objsDisabledInSpiritDim)
					{
						obj.SetActive(true);
					}
					foreach (GameObject obj in objsEnabledInSpiritDim)
					{
						obj.SetActive(false);
					}
				}

				hasDimSwitched = false;
			}
		}
	}

	// Uncomment to enable save-on-application-exit
	/*private void OnApplicationQuit()
	{
		SaveLoad.Save("zduncan3AgileEntry");
	}*/
}