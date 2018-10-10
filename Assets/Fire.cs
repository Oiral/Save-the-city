using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public List<Block> blocksOnFire;

    public List<Block> adjacentBlocks;

    public GameObject adjacentPrefab;

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
}
