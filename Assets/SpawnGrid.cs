using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour {
    public GameObject boxPrefab;

    public TextAsset file;

	// Use this for initialization
	void Start () {
        string[,] grid = ReadCSV.SplitGrid(file.text);

        for (int x = 0; x < grid.GetUpperBound(0); x++)
        {
            for (int z = 0; z < grid.GetUpperBound(1); z++)
            {
                if (grid[x, z] != "")
                {
                    int y = int.Parse(grid[x, z]);

                    GameObject objectInstance = Instantiate(boxPrefab);
                    objectInstance.transform.position = new Vector3(x, y, z);
                }
            }
        }
	}
}
