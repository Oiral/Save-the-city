using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public List<PlayerDetails> playerSquads = new List<PlayerDetails>();

    public PlayerDetails station;

    public int level;

    public void ToggleSquadType(int num)
    {
        switch (playerSquads[num].type)
        {
            case PlayerType.Money:
                playerSquads[num].type = PlayerType.Votes;
                Debug.Log("Vote");
                break;
            case PlayerType.Votes:
                playerSquads[num].type = PlayerType.Money;
                Debug.Log("Money");
                break;
        }
    }

    public void RefreshSquads()
    {
        for (int i = 0; i < playerSquads.Count; i++)
        {
            playerSquads[i].dead = false;
        }
    }
}
