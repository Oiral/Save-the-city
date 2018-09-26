﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public CornerMovementScript selectedPlayer;

    public Material selectedMat;
    public Material unSelectedMat;

    public GameObject firePrefab;

    [HideInInspector]
    public List<Block> blocksOnFire = new List<Block>();

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
        
        if (blocksOnFire.Count > 2)
        {
            SpreadRandomFire();
        }
        else
        {
            MakeNewFire();
        }


    }

    public void MakeNewFire()
    {
        //find a empty screen
        List<GameObject> blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blocks"));

        bool lookingForPlace = true;
        while (lookingForPlace)
        {
            int randnum = Random.Range(0, blocks.Count);
            Block block = blocks[randnum].GetComponent<Block>();

            if (block.onFire == false)
            {
                
                lookingForPlace = false;
                SetBlockOnFire(block);
            }
            blocks.Remove(block.gameObject);

            //check if all are full
            if (blocks.Count == 0)
            {
                lookingForPlace = false;
            }
        }
    }

    public void SpreadRandomFire()
    {
        //Pick a random block that is already on fire
        Block randomBlock = blocksOnFire[Random.Range(0, blocksOnFire.Count)];

        //Pick a random corner from the block
        List<Corner> eligableCorners = randomBlock.connectedCorners;
        bool lookingForSpread = true;

        while (lookingForSpread)
        {
            Corner randomCorner = eligableCorners[Random.Range(0, randomBlock.connectedCorners.Count)];
            List<Block> cornerBlocks = randomCorner.connectedBlocks;
            //remove the random block because we dont want to try spread to the block were spreading from
            cornerBlocks.Remove(randomBlock);
            if (cornerBlocks.Count > 0)
            {
                //We still have another block to spread too
                //Pick a random block to spread too now
                
                int RandNum = Random.Range(0, cornerBlocks.Count);
                if (cornerBlocks[RandNum].onFire == false)
                {
                    SetBlockOnFire(cornerBlocks[RandNum]);
                    lookingForSpread = false;
                }
                
            }
        }
        

        //Get the connected blocks


    }

    public void SetBlockOnFire(Block blockToIgnite)
    {
        Debug.Log("Setting Block on fire");
        blockToIgnite.SetOnFire();
        blocksOnFire.Add(blockToIgnite);
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
