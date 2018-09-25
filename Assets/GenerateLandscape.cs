using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLandscape : MonoBehaviour {

    public GameObject cornerPrefab;
    public GameObject blockPrefab;

    public int xSize;
    public int zSize;

    public float scale = 10;
    public float seed = 1;

    public float gridDistance = 1;

    public void Start()
    {
        Vector3[,] verticies = new Vector3[xSize , zSize];

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                if (Mathf.PerlinNoise(x * scale + seed, z * scale + seed) > 0f)
                {
                    //Spawn in the corner prefab
                    //Debug.Log(Mathf.PerlinNoise(x * scale + seed, z * scale + seed));
                    Instantiate(cornerPrefab, new Vector3(gridDistance * x, 0, gridDistance * z), Quaternion.identity, transform);
                    verticies[x,z] = new Vector3(gridDistance * x, 0, gridDistance * z);
                }
            }
        }
        //Landscape has been generated
        for (int x = 1; x < xSize; x++)
        {
            for (int z = 1; z < zSize; z++)
            {
                //(a + b + c + d)/4
                Vector3 avgPos =(
                    verticies[x - 1, z - 1] +
                    verticies[x    , z - 1] +
                    verticies[x - 1, z    ] +
                    verticies[x    , z    ])*0.25f;
                

                GameObject block = Instantiate(blockPrefab,avgPos,Quaternion.Euler(0,0,180),transform);
                Mesh blockMesh = new Mesh();
                blockMesh = DrawMesh.AddNewQuad(blockMesh, verticies[x, z] - avgPos, verticies[x - 1, z] - avgPos, verticies[x, z - 1] - avgPos, verticies[x - 1, z - 1] - avgPos);
                block.GetComponent<MeshFilter>().mesh = blockMesh;
                //block.transform.Rotate(Vector3.forward, 180);

                block.GetComponent<MeshCollider>().sharedMesh = blockMesh;
            }
        }

    }
    
}
