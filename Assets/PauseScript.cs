using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {


    #region Singleton

    public PauseScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this);
        }
    }

    #endregion

    public bool paused;

    public GameObject inGameCanvas;
    public GameObject placementCanvas;
    public GameObject pauseCanvas;
    

    public void TogglePause()
    {
        paused = !paused;

        switch (LevelManager.instance.phase)
        {
            case Phase.Placement:
                placementCanvas.SetActive(!paused);
                break;
            case Phase.PlayerTurn:
                inGameCanvas.SetActive(!paused);
                break;
            case Phase.FireSpread:
                inGameCanvas.SetActive(!paused);
                break;
            default:
                break;
        }
        pauseCanvas.SetActive(paused);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }
}
