using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public List<Block> blocksOnFire;

    public List<Block> adjacentBlocks;

    public GameObject adjacentPrefab;

    private void Start()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Blocks");
        blocksOnFire.Add( objects[15].GetComponent<Block>());

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
                Instantiate(adjacentPrefab, addBlock.transform.position, Quaternion.identity, transform);
            }
        }
    }

    public void ExpandFire()
    {
        //Pick a random adjacent block
        Block randomAdjacent = adjacentBlocks[Random.Range(0, adjacentBlocks.Count)];

        //Remove each count of random adjacent from adjacent blocks
        while (adjacentBlocks.Contains(randomAdjacent))
        {
            adjacentBlocks.Remove(randomAdjacent);
        }

        //add the random block to the fire
        blocksOnFire.Add(randomAdjacent);

        //add each adjacent block of random adjacent to our adjacent blocks
        AddAdjacentBlocks(randomAdjacent.connectedBlocks);
        randomAdjacent.SetOnFire();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ExpandFire();
        }
    }
}
