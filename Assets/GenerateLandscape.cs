using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLandscape : MonoBehaviour {

    public GameObject cornerPrefab;
    public GameObject blockPrefab;

    public int xSize = 15;
    public int zSize = 15;

    public float scale = 0.8f;
    public float seed = 1;

    public float gridDistance = 2;

    [Range(0,1)]
    public float removeChance = 0.55f;

    public bool generateOnStart = false;

    private GameObject blockParent;
    private GameObject cornerParent;

    public void Awake()
    {
        if (generateOnStart)
        {
            Generate();
        }
    }

    [ContextMenu("Generate the landscape")]
    public void Generate()
    {
        if (blockParent != null)
        {
            DestroyImmediate(blockParent);
        }
        if (cornerParent != null)
        {
            DestroyImmediate(cornerParent);
        }

        Vector3[,] verticies = new Vector3[xSize, zSize];
        GameObject[,] corners = new GameObject[xSize, zSize];

        //make a new empty game object for the parents of the corners and blocks
        blockParent = new GameObject();
        blockParent.transform.name = "Block Parent";

        cornerParent = new GameObject();
        cornerParent.transform.name = "Corner Parent";

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                //Spawn in the corner prefab
                //Debug.Log(Mathf.PerlinNoise(x * scale + seed, z * scale + seed));
                corners[x, z] = Instantiate(cornerPrefab, new Vector3(gridDistance * x, 0, gridDistance * z), Quaternion.identity, cornerParent.transform);

                verticies[x, z] = new Vector3(gridDistance * x, 0, gridDistance * z);
            }
        }

        //Generate the links between all the corners
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                Corner currentCorner = corners[x, z].GetComponent<Corner>();

                if (x > 0)
                {
                    //Add the x-1 node
                    currentCorner.connectedCorners.Add(corners[x - 1, z].GetComponent<Corner>());
                }
                if (z > 0)
                {
                    //Add the z-1 node
                    currentCorner.connectedCorners.Add(corners[x, z - 1].GetComponent<Corner>());
                }
                if (z < zSize - 1)
                {
                    //Add the z+1 node
                    currentCorner.connectedCorners.Add(corners[x, z + 1].GetComponent<Corner>());
                }
                if (x < xSize - 1)
                {
                    //Add the x+1 node
                    currentCorner.connectedCorners.Add(corners[x + 1, z].GetComponent<Corner>());
                }

            }
        }



        List<Block> generatedBlocks = new List<Block>();
        //Landscape has been generated
        for (int x = 1; x < xSize; x++)
        {
            for (int z = 1; z < zSize; z++)
            {
                //(a + b + c + d)/4
                Vector3 avgPos = (
                    verticies[x - 1, z - 1] +
                    verticies[x, z - 1] +
                    verticies[x - 1, z] +
                    verticies[x, z]) * 0.25f;


                GameObject block = Instantiate(blockPrefab, avgPos, Quaternion.Euler(0, 0, 180), blockParent.transform);

                Mesh blockMesh = new Mesh();

                blockMesh = DrawMesh.AddNewQuad(blockMesh, verticies[x, z] - avgPos, verticies[x - 1, z] - avgPos, verticies[x, z - 1] - avgPos, verticies[x - 1, z - 1] - avgPos);

                block.GetComponent<MeshFilter>().mesh = blockMesh;
                //block.transform.Rotate(Vector3.forward, 180);

                block.GetComponent<MeshCollider>().sharedMesh = blockMesh;

                //Generate the links to each of the corners the block is attached too
                block.GetComponent<Block>().connectedCorners = new List<Corner>
                {
                    corners[x - 1, z - 1].GetComponent<Corner>(),
                    corners[x, z - 1].GetComponent<Corner>() ,
                    corners[x - 1, z].GetComponent<Corner>() ,
                    corners[x, z].GetComponent<Corner>()
                };

                //give each corner attached this block to add
                Block thisBlock = block.GetComponent<Block>();
                corners[x - 1, z - 1].GetComponent<Corner>().connectedBlocks.Add(thisBlock);
                corners[x, z - 1].GetComponent<Corner>().connectedBlocks.Add(thisBlock);
                corners[x - 1, z].GetComponent<Corner>().connectedBlocks.Add(thisBlock);
                corners[x, z].GetComponent<Corner>().connectedBlocks.Add(thisBlock);

                generatedBlocks.Add(thisBlock);

            }
        }

        
        //Generate all the links to the neighboring blocks
        foreach (Block currentBlock in generatedBlocks)
        {
            
            foreach (Corner corner in currentBlock.connectedCorners)
            {
                foreach (Block connectTo in corner.connectedBlocks)
                {
                    if (connectTo != currentBlock && !currentBlock.connectedBlocks.Contains(connectTo))
                    {
                            currentBlock.connectedBlocks.Add(connectTo);
                    }
                }
            }
        }

        //Remove some random paths
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                if (Mathf.PerlinNoise(((x + scale) * scale) + seed, ((z + scale) * scale) + seed ) > removeChance)
                {
                    //Remove the linked corners
                    foreach (Corner AttachedCorner in corners[x,z].GetComponent<Corner>().connectedCorners)
                    {
                        AttachedCorner.connectedCorners.Remove(corners[x, z].GetComponent<Corner>());
                        //check if the attached corner has no more corners its attached too
                        if (AttachedCorner.connectedCorners.Count == 0)
                        {
                            //Remove the linked blocks from this corner
                            RemoveLinkedBlocks(AttachedCorner);
                            DestroyImmediate(AttachedCorner.gameObject);
                        }
                    }

                    RemoveLinkedBlocks(corners[x, z].GetComponent<Corner>());

                    DestroyImmediate(corners[x, z].gameObject);
                }
            }
        }
    }

    void RemoveLinkedBlocks(Corner corner)
    {
        //Remove the linked Blocks
        foreach (Block AttachedBlock in corner.connectedBlocks)
        {
            AttachedBlock.connectedCorners.Remove(corner);
            //if the attached block now has no more corners destroy that block
            if (AttachedBlock.connectedCorners.Count <= 2)
            {
                foreach (Corner attachedCorner in AttachedBlock.connectedCorners)
                {
                    attachedCorner.connectedBlocks.Remove(AttachedBlock);
                }

                //Remove each connected block
                foreach (Block connectedBlock in AttachedBlock.connectedBlocks)
                {
                    connectedBlock.connectedBlocks.Remove(AttachedBlock);
                }

                DestroyImmediate(AttachedBlock.gameObject);
            }
        }
    }
}
