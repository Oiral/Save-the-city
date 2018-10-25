using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFunctions : MonoBehaviour {

    public int loadedScene;

    public Text levelNum;

    private void Awake()
    {
        loadedScene = SceneManager.GetActiveScene().buildIndex;
        if (levelNum != null)
        {
            levelNum.text = (loadedScene - 1).ToString();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(loadedScene += 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(loadedScene);
    }

    public void LoadLevel(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
