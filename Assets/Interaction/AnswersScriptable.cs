using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { LoseTeam, GainMoney, LoseMoney };

[CreateAssetMenu(fileName = "Answer", menuName = "Interaction/Answer", order = 1)]
public class AnswersScriptable : ScriptableObject {
    [Multiline]
    public string description;
    public Action actionToTake;
    public int actionNumber;

    [Multiline]
    public string chosenDescription;


    public string GetChosenDesc()
    {
        string text = chosenDescription;

        return text.Replace("{num}", actionNumber.ToString());
    }
}
