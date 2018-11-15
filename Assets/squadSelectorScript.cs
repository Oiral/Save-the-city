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
        for (int i = 0; i < GameManager.instance.playerSquads.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);

            Button buttonScript = button.GetComponent<Button>();

            int num = i;

            buttonScript.onClick.AddListener(() => { selectButton(num); });

            Text buttonText = button.GetComponentInChildren<Text>();

            buttonText.text = "Squad " + (i + 1).ToString();
            

            buttons.Add(buttonScript);

        }

        selectButton(0);
    }

    public void selectButton(int number)
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
        placementScript.setSelectedPlayer(number);
    }
}
