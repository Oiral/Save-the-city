﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public List<Block> blocksOnFire;

    public List<Block> adjacentBlocks;

    public GameObject adjacentPrefab;

    public List<SpreadMarker> spreadMarkers = new List<SpreadMarker>();

    private void Start()
    {
        GameManager.instance.fire = this;

        foreach (InfernoTower towers in GameManager.instance.infernoTowers)
        {
            blocksOnFire.Add(towers.attachedBlock);
        }

        foreach (Block block in blocksOnFire)
        {
            AddAdjacentBlocks(block.connectedBlocks);
            block.SetOnFire();
        }

        GenerateSpreadMarkers();
    }

    public void AddAdjacentBlocks(List<Block> blocksToAdd)
    {
        foreach (Block addBlock in blocksToAdd)
        {
            if (!blocksOnFire.Contains(addBlock))
            {
                adjacentBlocks.Add(addBlock);
                //Instantiate(adjacentPrefab, addBlock.transform.position, Quaternion.identity, transform);
            }
        }
    }

    /*
    public void ExpandFire()
    {
        //Pick a random adjacent block
        Block randomAdjacent = adjacentBlocks[Random.Range(0, adjacentBlocks.Count)];

        if (randomAdjacent.blockerOnBlock)
        {
            ExpandFire();
            return;
        }

        //Remove each count of random adjacent from adjacent blocks
        while (adjacentBlocks.Contains(randomAdjacent))
        {
            adjacentBlocks.Remove(randomAdjacent);
        }

        //add the random block to the fire
        blocksOnFire.Add(randomAdjacent);

        //If its the inferno tower
        if (randomAdjacent.infernoTower != null)
        {
            GameManager.instance.infernoTowers.Add(randomAdjacent.infernoTower);
        }

        //add each adjacent block of random adjacent to our adjacent blocks
        AddAdjacentBlocks(randomAdjacent.connectedBlocks);
        randomAdjacent.SetOnFire();
    }*/
    public void ExpandFire()
    {
        foreach (SpreadMarker marker in spreadMarkers)
        {
            if (marker.attachedBlock.blockerOnBlock == false)
            {
                //Remove each count of random adjacent from adjacent blocks
                while (adjacentBlocks.Contains(marker.attachedBlock))
                {
                    adjacentBlocks.Remove(marker.attachedBlock);
                }

                //add the random block to the fire
                blocksOnFire.Add(marker.attachedBlock);

                //If its the inferno tower
                if (marker.attachedBlock.infernoTower != null)
                {
                    GameManager.instance.infernoTowers.Add(marker.attachedBlock.infernoTower);
                }

                //add each adjacent block of random adjacent to our adjacent blocks
                AddAdjacentBlocks(marker.attachedBlock.connectedBlocks);
                marker.attachedBlock.SetOnFire();
            }
        }
        RemoveSpreadMarkers();
        GenerateSpreadMarkers();
    }

    public void LightFire(Block blockToLight)
    {
        //add the random block to the fire
        blocksOnFire.Add(blockToLight);
    }

    public void RemoveBlock(Block blockToRemove)
    {
        foreach (Block connectBlock in blockToRemove.connectedBlocks)
        {
            //Remove each case of this adjacent block
            if (adjacentBlocks.Contains(connectBlock))
            {
                adjacentBlocks.Remove(connectBlock);
            }
            //check if any blocks around are on fire
            if (connectBlock.onFire)
            {
                //add to the adjacent block
                adjacentBlocks.Add(blockToRemove);
            }
        }
        blocksOnFire.Remove(blockToRemove);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ExpandFire();
        }
    }
    //Pick and random block to ignite
    //Show the player its going to ignite
    
    //Next click light the current one on fire

    public void GenerateSpreadMarkers()
    {
        Debug.Log("Generating stuff");
        //Loop through with how many towers there are
        for (int i = 0; i < GameManager.instance.infernoTowers.Count + 1; i++)
        {
            //only try to place 10 times until it gives up trying to place a block
            for (int x = 0; x < 10; x++)
            {
                //Pick a random block
                Block randomAdjacent = adjacentBlocks[Random.Range(0, adjacentBlocks.Count)];

                //if there is not already a marker on this spot
                //and if there is no blocker on the block
                if (!MarkersContains(randomAdjacent) && !randomAdjacent.blockerOnBlock)
                {
                    //Create the visual
                    GameObject marker = Instantiate(adjacentPrefab, transform);

                    //Move the visual to the position
                    marker.transform.position = randomAdjacent.transform.position + Vector3.up * 0.1f;

                    //Create the info for the marker
                    SpreadMarker spreadMarker = new SpreadMarker(randomAdjacent, marker);

                    //Add the marker to the list of markers
                    spreadMarkers.Add(spreadMarker);

                    break;
                }
                
            }

        }
        
    }

    public void RemoveSpreadMarkers()
    {
        foreach (SpreadMarker marker in spreadMarkers)
        {
            Destroy(marker.relatedObject);
        }
        spreadMarkers = new List<SpreadMarker>();
    }

    public bool MarkersContains(Block blockToCheck)
    {
        for (int i = 0; i < spreadMarkers.Count; i++)
        {
            if (spreadMarkers[i].attachedBlock == blockToCheck)
            {
                return true;
            }
        }
        return false;
    }

    public bool MarkersContains(SpreadMarker markerToCheck)
    {
        for (int i = 0; i < spreadMarkers.Count; i++)
        {
            if (spreadMarkers[i].attachedBlock == markerToCheck.attachedBlock)
            {
                return true;
            }
        }
        return false;
    }
}

public class SpreadMarker
{
    public Block attachedBlock;
    public GameObject relatedObject;

    public SpreadMarker(Block block,GameObject attachedObject)
    {
        attachedBlock = block;
        relatedObject = attachedObject;
    }
}
