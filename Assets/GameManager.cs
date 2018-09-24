using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public CornerMovementScript selectedPlayer;

    public Material selectedMat;
    public Material unSelectedMat;

    public GameObject firePrefab;

    public void MovePlayer(Corner cornerToMoveTo)
    {
        if (selectedPlayer != null)
        {
            selectedPlayer.NextCorner(cornerToMoveTo);
        }
    }

    public void CheckBlock(Block blockToCheck)
    {
        if (selectedPlayer.CanRemoveFire(blockToCheck))
        {
            blockToCheck.RemoveFire();
            //set a corner to be disabled
            //Pick a random corner

            List<Corner> corners = blockToCheck.connectedCorners;

            bool lookingForPlace = true;
            while (lookingForPlace)
            {
                int randnum = Random.Range(0, corners.Count);
                Corner corner = corners[randnum].GetComponent<Corner>();

                if (corner.blocked == false)
                {
                    corner.BlockCorner();
                    corner.GetComponent<MeshRenderer>().material = unSelectedMat;
                    lookingForPlace = false;
                }
                corners.Remove(corner);

                //check if all are full
                if (corners.Count == 0)
                {
                    lookingForPlace = false;
                }
            }
        }
    }

	public void NextTurn()
    {
        Debug.Log("Next Turn");

        //Reset the action points
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<CornerMovementScript>().ResetActionPoints();
        }

        //Create a new fire
        //find a empty screen
        List<GameObject> blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blocks"));

        bool lookingForPlace = true;
        while (lookingForPlace)
        {
            int randnum = Random.Range(0, blocks.Count);
            Block block = blocks[randnum].GetComponent<Block>();

            if (block.onFire == false)
            {
                block.SetOnFire();
                lookingForPlace = false;
            }
            blocks.Remove(block.gameObject);

            //check if all are full
            if (blocks.Count == 0)
            {
                lookingForPlace = false;
            }
        }

    }

    public void SelectPlayer(CornerMovementScript player)
    {
        if (selectedPlayer != null)
        {
            //Reset the material on the select player
            selectedPlayer.gameObject.GetComponent<MeshRenderer>().material = unSelectedMat;
        }

        //select the player
        selectedPlayer = player;
        selectedPlayer.gameObject.GetComponent<MeshRenderer>().material = selectedMat;
    }
}
