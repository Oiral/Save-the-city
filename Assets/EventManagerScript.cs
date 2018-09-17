using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour {

    public TextAsset csvFile;

    private void Start()
    {
        string[,] grid = ReadCSV.SplitGrid(csvFile.text);
        grid = ReadCSV.TrimArray(0, grid, 0);
        //ReadCSV.DebugArray(grid);

        //The position of each event within the file
        int[] eventNumber = ReadCSV.GetCollumnItems(0,grid);
        //Debug.Log(eventNumber[2]);

        int randnum = Random.Range(0, eventNumber.Length);

        //Pick a random event
        InteractionEvent currentEvent = new InteractionEvent(randnum, grid);
        

        
        Debug.Log(currentEvent.eventNumber);
        Debug.Log(currentEvent.positionInCollumn);
        Debug.Log(grid[1, currentEvent.eventNumber]);
        Debug.Log(grid[2, currentEvent.option1]);
        Debug.Log(grid[2, currentEvent.option2]);
        Debug.Log(grid[3, currentEvent.reaction1]);
        Debug.Log(grid[3, currentEvent.reaction2]);

    }
}
