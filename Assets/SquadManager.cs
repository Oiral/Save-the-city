using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour {
    
    public GameObject SquadUIPrefab;

    public List<Squad> squads;

    public Squad activeSquad;

    public void NewSquad()
    {
        GameObject SquadUI = Instantiate(SquadUIPrefab, transform);
        squads.Add(SquadUI.GetComponent<Squad>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            NewSquad();
        }
    }
}
