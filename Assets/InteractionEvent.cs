using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum reactionEnum {Money,Squad}

public class InteractionEvent{

    public int positionInCollumn;
    public int eventNumber;

    public int option1;
    public int reaction1;
    public reactionEnum reaction1Option;
    public int reaction1Count;

    public int option2;
    public int reaction2;
    public reactionEnum reaction2Option;
    public int reaction2Count;



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

        reaction1Option = checkReaction(array[6, option1]);
        reaction2Option = checkReaction(array[6, option2]);

        reaction1Count = System.Convert.ToInt32(array[5, option1]);
        reaction2Count = System.Convert.ToInt32(array[5, option2]);

    }

    private reactionEnum checkReaction(string input)
    {
        reactionEnum returnValue = reactionEnum.Money;

        if (input == "Money")
        {
            returnValue = reactionEnum.Money;
        }
        if (input == "Squad")
        {
            returnValue = reactionEnum.Squad;
        }
        return returnValue;
    }
    
}
