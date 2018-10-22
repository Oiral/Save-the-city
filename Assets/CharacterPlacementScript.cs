using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacementScript : MonoBehaviour {

    public int currentSelectedPlayer;

    public List<PlayerDetails> squadMembers;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            bool cornerClicked = false;
            bool blockClicked = false;
            bool startingPanelClicked = false;

            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                switch (hit.transform.tag)
                {
                    case "Corner":
                        cornerClicked = true;
                        break;
                    case "Blocks":
                        blockClicked = true;
                        break;

                    case "Start":
                        startingPanelClicked = true;
                        break;
                }
            }

            if (startingPanelClicked && cornerClicked)
            {
                if (squadMembers[currentSelectedPlayer].playerMovementType == MovementType.Corner)
                {
                    //Spawn in the corner squad member
                }
            }else if (startingPanelClicked && blockClicked)
            {
                if (squadMembers[currentSelectedPlayer].playerMovementType == MovementType.Block)
                {
                    //Spawn in the block squad member
                }
            }
        }
    }
}
