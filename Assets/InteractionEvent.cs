using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent{

    public int positionInCollumn;
    public int eventNumber;

    public int option1;
    public int reaction1;

    public int option2;
    public int reaction2;



	public InteractionEvent(int number,string[,]array)
    {
        eventNumber = number;

        //Get the total events
        int[] events = ReadCSV.GetCollumnItems(0, array);
        positionInCollumn = events[number];
        int nextEventRow;
        if (number == events.Length-1)
        {
            nextEventRow = array.GetLength(1) + 1;
        }
        else
        {
            nextEventRow = events[number + 1];
        }

        //get the total options
        int[] temp = ReadCSV.GetCollumnItemBetween(array, 2, positionInCollumn, nextEventRow - 1);
        List<int> options = new List<int>();
        foreach (int i in temp)
        {
            options.Add(i);
        }

        //Randomise the two options and make sure we cant 
        int num = (int)Random.Range(0, options.Count);
        option1 = options[num];
        options.RemoveAt(num);

        num = Random.Range(0, options.Count);
        option2 = options[num];
        options.RemoveAt(num);

        //Get reaction for option 1
        reaction1 = option1 + (int)Random.Range(0, 2);
        reaction2 = option2 + (int)Random.Range(0, 2);
    }
    
}
