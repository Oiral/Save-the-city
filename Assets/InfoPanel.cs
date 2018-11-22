using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    [Header("Slide")]
    public RectTransform panel;

    public bool active;

    LevelManager level;

    public float slideSpeed;
    public float waitTime;

    PlayerMovementScript player;

    public Vector2 slideAwayPos;

    [Header("Info")]
    public Text healthText;
    public Text maxHealthText;

    public Text movesText;
    public Text maxMovesText;
    public bool toBeBurned;

    private void Start()
    {
        level = LevelManager.instance;
    }

    private void Update()
    {
        if (level.selectedPlayer != player)
        {
            StartCoroutine(SwapPlayers());
            player = level.selectedPlayer;
        }


        //Slide the panel
        if (active)
        {
            //slide towards 0,0
            panel.transform.localPosition = Vector2.Lerp(panel.transform.localPosition, Vector2.zero, slideSpeed * Time.deltaTime);
            SetInfo();
        }
        else
        {
            //slide towards target pos
            panel.transform.localPosition = Vector2.Lerp(panel.transform.localPosition, slideAwayPos, (slideSpeed * 2) * Time.deltaTime);
        }
    }

    IEnumerator SwapPlayers()
    {
        if (player != null)
        {
            
            //Slide old info down
            active = false;
            yield return new WaitForSeconds(waitTime/2);
        }


        //replace the info on the screen
        //SetInfo();

        yield return new WaitForSeconds(waitTime / 2);
        //slide new info up
        active = true;
    }

    public void SetInfo()
    {
        healthText.text = player.currentHealth.ToString();
        maxHealthText.text = player.maxHealth.ToString();

        movesText.text = player.actionPoints.ToString();
        maxMovesText.text = player.maxActionPoints.ToString();
    }
}
