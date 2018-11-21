using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelGenerator : MonoBehaviour {

    Mesh mesh;
    Vector3[] verticies;
    int[] triangles;

    public float cellSize;
    public Vector3 gridOffset;
    public int gridSize;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        MakeContiguousProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid()
    {
        //Set array sizes
        verticies = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];


        //tracker ints
        int v = 0;
        int t = 0;

        //set vertex offset
        float offset = cellSize * 0.5f;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize, 0, y * cellSize);

                //populate the verticies and triangles array
                verticies[v] = new Vector3(-offset, 0, -offset) + cellOffset + gridOffset;
                verticies[v + 1] = new Vector3(-offset, 0, offset) + cellOffset + gridOffset;
                verticies[v + 2] = new Vector3(offset, 0, -offset) + cellOffset + gridOffset;
                verticies[v + 3] = new Vector3(offset, 0, offset) + cellOffset + gridOffset;

                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;
            }
        }
    }

    void MakeContiguousProceduralGrid()
    {
        //Set array sizes
        verticies = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];


        //tracker ints
        int v = 0;
        int t = 0;

        //set vertex offset
        float offset = cellSize * 0.5f;

        //create the vertex grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                verticies[v] = new Vector3((x * cellSize) - offset, 0, (y * cellSize) - offset);
                v++;
            }
        }

        //reset the vertex tracker
        v = 0;

        //setting each cell's triangles
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 1) + 1;
                v++;
                t += 6;
            }
            v++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


}
