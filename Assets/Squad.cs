using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SquadType {General,Fire,Rescue,Demolishion };

public class Squad : MonoBehaviour{

    public SquadType typeOfSquad = SquadType.General;

    public bool onMission = false;

    public int exp;

    public int level = 0;

    public GameObject disableObject;
    public Text squadName;

    Button squadButton;

    ColorBlock baseColour;
    public ColorBlock selectedBlockColours;

    public bool selected;

    SquadManager manager;

    private void Start()
    {
        squadButton = GetComponent<Button>();
        baseColour = squadButton.colors;
        manager = transform.parent.GetComponent<SquadManager>();

        squadName.text = "Squad " + manager.squads.Count;
    }

    public void SquadSelected()
    {
        //Deselect every other button attached to parent
        foreach (Squad sqaud in manager.squads)
        {
            sqaud.SquadDeselect();
        }

        manager.activeSquad = this;

        squadButton.colors = selectedBlockColours;

        selected = true;
        
    }

    public void SquadDeselect()
    {
        squadButton.colors = baseColour;
        selected = false;
    }

    public void GoToMission()
    {
        onMission = true;

        disableObject.SetActive(onMission);
        squadButton.interactable = !onMission;
    }
}
