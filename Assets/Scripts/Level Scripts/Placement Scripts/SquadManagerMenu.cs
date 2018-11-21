using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadManagerMenu : MonoBehaviour {

    public GameObject ButtonPrefab;

    GameManager manager;

    private void Start()
    {
        manager = GameManager.instance;
        for (int i = 0; i < manager.playerSquads.Count; i++)
        {
            GameObject buttonObject = Instantiate(ButtonPrefab, transform);
            buttonObject.GetComponent<SquadButtonMenu>().num = i;
        }
    }

    
}
