using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class squadSelectorScript : MonoBehaviour {

    public List<Button> buttons;


    public Color placedColour;
    public Color activeColor;

    public GameObject buttonPrefab;
    CharacterPlacementScript placementScript;
    private void Start()
    {
        placementScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<CharacterPlacementScript>();

        GameObject stationButton = Instantiate(buttonPrefab, transform);

        Button stationButtonScript = stationButton.GetComponent<Button>();

        stationButtonScript.onClick.AddListener(() => { selectButton(0,true); });

        Text stationButtonText = stationButton.GetComponentInChildren<Text>();
        stationButtonText.text = "Station ";

        buttons.Add(stationButtonScript);

        for (int i = 0; i < GameManager.instance.playerSquads.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);

            Button buttonScript = button.GetComponent<Button>();

            int num = i + 1;

            buttonScript.onClick.AddListener(() => { selectButton(num,false); });

            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = "Squad " + (i + 1).ToString();

            buttons.Add(buttonScript);

        }

        selectButton(0,true);
    }

    public void selectButton(int number,bool selection)
    {
        Debug.Log("Select");
        for (int i = 0; i < buttons.Count; i++)
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
        if (selection == true)
        {

        }
        else
        {
            placementScript.setSelectedPlayer(number);
        }
    }
}
