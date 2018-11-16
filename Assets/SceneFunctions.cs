﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class SceneFunctions : MonoBehaviour {

    public int loadedScene;

    public Text levelNum;

    private void Awake()
    {
        loadedScene = SceneManager.GetActiveScene().buildIndex;
        if (levelNum != null)
        {
            levelNum.text = (loadedScene + 1).ToString();
        }
    }

    public void NextLevel()
    {

        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(loadedScene += 1);
        if (FindObjectOfType<LevelManager>() != null)
        {
            Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "turns", LevelManager.instance.turnCount }
        });
        }
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
