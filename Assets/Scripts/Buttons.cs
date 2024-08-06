using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public static Buttons instance;

    public List<GameObject> menuPanels;

    [HideInInspector] public GameObject lastOpenedMenu;

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

        if (menuPanels.Count > 0)
		    lastOpenedMenu = menuPanels[0];

        foreach (var panel in menuPanels)
        {
            panel.SetActive(true);
            panel.SetActive(false);
        }
	}

	public void LoadScene(string sceneToLoad)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }

    public void ButtonBackToPlaying()
    {
		Time.timeScale = 1.0f;
		GameLogic.instance.isMenuOpen = false;

        foreach (var panel in menuPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void ChangeLastOpenedMenu(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Last Opened Menu cannot be set to null");
            lastOpenedMenu = menuPanels[0];
            return;
        }

        lastOpenedMenu = obj;
    }

    public void SaveGame()
    {
		SaveLoad.Save("zduncan3AgileEntry");
	}

    public void ButtonQuitGame()
    {
        SaveGame();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}