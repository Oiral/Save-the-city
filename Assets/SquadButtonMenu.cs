using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadButtonMenu : MonoBehaviour {

	public int num = 0;

    public Color moneyColor;
    public Color voteColor;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        ColorBlock block = button.colors;

        if (GameManager.instance.playerSquads[num].type == PlayerType.Money)
        {
            //set the color to money
            block.normalColor = moneyColor;
        }
        else
        {
            //set the color to vote
            block.normalColor = voteColor;
        }

        button.colors = block;
    }

    public void SwapType()
    {
        GameManager.instance.ToggleSquadType(num);
    }

}
