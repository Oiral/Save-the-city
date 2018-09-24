using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLandscape : MonoBehaviour {

    public GameObject cornerPrefab;

    public int xSize;
    public int zSize;

    public float scale = 10;
    public float seed = 1;

    public void Start()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                if (Mathf.PerlinNoise(x * scale + seed, z * scale + seed) > 0.5f)
                {
                    //Spawn in the corner prefab
                    Debug.Log(Mathf.PerlinNoise(x * scale + seed, z * scale + seed));
                    Instantiate(cornerPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
                }
            }
        }
    }
    
}
