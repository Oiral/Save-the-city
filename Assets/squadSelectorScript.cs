using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class squadSelectorScript : MonoBehaviour {

    public Button[] buttons;


    public Color placedColour;
    public Color activeColor;
    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        selectButton(0);
    }

    public void selectButton(int number)
    {
        CharacterPlacementScript placementScript = GameObject.FindGameObjectWithTag("Start").GetComponent<CharacterPlacementScript>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (placementScript.spawned.Contains(GameManager.instance.playerSquads[i]) == false)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
                ColorBlock col = buttons[i].colors;
                col.disabledColor = placedColour;
                buttons[i].colors = col;
            }
        }
        buttons[number].interactable = false;
        placementScript.setSelectedPlayer(number);
    }
}
