using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManagerScript : MonoBehaviour {

    public TextAsset csvFile;

    string[,] grid = new string[0,0];
    int[] eventsPositions = new int[0];
    InteractionEvent currentEvent;

    public GameObject InteractionCanvas;
    public Text title;
    public Text description;

    [Header("Buttons")]
    public GameObject buttonsPanel;
    public Text option1;
    public Text option2;

    [Header("Reactions")]
    public GameObject reactionPanel;
    public Text reaction;
    public Text reward;

    private void Start()
    {
        grid = ReadCSV.SplitGrid(csvFile.text);
        grid = ReadCSV.TrimArray(0, grid, 0);
        //ReadCSV.DebugArray(grid);

        //The position of each event within the file

        //NextEvent();
        eventsPositions = ReadCSV.GetCollumnItems(0, grid);
    }

    public void Update()
    {
        //Debug.Log(eventNumber[2]);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextEvent();
        }
    }

    public void NextEvent()
    {

        InteractionCanvas.SetActive(true);
        int randnum = Random.Range(0, eventsPositions.Length);

        Debug.Log(grid);
        //Pick a random event
        currentEvent = new InteractionEvent(randnum, grid);

        if (title != null && description != null && option1 != null && option2 != null)
        {
            title.text = grid[0, currentEvent.positionInCollumn];
            description.text = grid[1, currentEvent.positionInCollumn];
            option1.text = grid[2, currentEvent.option1];
            option2.text = grid[2, currentEvent.option2];
        }

        //Debug.Log(currentEvent.eventNumber);
        //Debug.Log(currentEvent.positionInCollumn);
        Debug.Log(grid[1, currentEvent.positionInCollumn]);
        Debug.Log(grid[2, currentEvent.option1]);
        Debug.Log(grid[2, currentEvent.option2]);
        Debug.Log(grid[3, currentEvent.reaction1]);
        Debug.Log(grid[3, currentEvent.reaction2]);

        Debug.Log(currentEvent.reaction1Count);
        Debug.Log(currentEvent.reaction2Count);

        //hide the buttons
        buttonsPanel.SetActive(true);

        //show the reactionPanel
        reactionPanel.SetActive(false);
    }

    public void PickOption(int option)
    {
        int optionRowNum = 0;
        if (option == 1)
        {
            optionRowNum = currentEvent.option1;
        }
        else
        {
            optionRowNum = currentEvent.option2;
        }

        //hide the buttons
        buttonsPanel.SetActive(false);

        //show the reactionPanel
        reactionPanel.SetActive(true);
        reaction.text = grid[3, optionRowNum];
        reward.text = grid[4, optionRowNum];
    }

    public void ExitEvent()
    {
        InteractionCanvas.SetActive(false);
    }
}
