using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;

	private void Start()
	{
		if (GameLogic.instance == null)
        {
            LoadScene("MainMenu");
        }
	}

	public void LoadScene(string sceneName)
    {
		if (GameLogic.instance != null)
		{
			GameLogic.instance.objsDisabledInSpiritDim.Clear();
			GameLogic.instance.objsEnabledInSpiritDim.Clear();
		}

		StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(2.25f);

		AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

		while (!op.isDone)
        {
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}
