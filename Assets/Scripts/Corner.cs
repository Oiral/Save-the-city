using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {

    public List<Block> connectedBlocks;

    public List<Corner> connectedCorners;

    public GameObject streetPrefab;

    public bool blocked = false;

    public bool movable = true;

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().CheckCorner(this);
    }

    public void BlockCorner()
    {
        blocked = true;
    }

    public void UnBlockCorner()
    {
        blocked = false;
        GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().selectedMat;
    }

    private void Update()
    {
        bool allConnectedBlockOnFire = true;

        foreach (Block block in connectedBlocks)
        {
            if (block.onFire == false)
            {
                allConnectedBlockOnFire = false;
            }
        }

        movable = !allConnectedBlockOnFire;
        
    }

    public void Start()
    {
        //Spawn in the roads
        foreach (Corner connectedCorner in connectedCorners)
        {
            GameObject street = Instantiate(streetPrefab, transform.position, Quaternion.identity, transform);
            //make the street look at the connected corner
            street.transform.LookAt(connectedCorner.transform.position, Vector3.up);

            //Resize the scale of the street
            Vector3 scale = street.transform.localScale;
            scale.z = Vector3.Distance(transform.position, connectedCorner.transform.position);
            street.transform.localScale = scale;
        }
    }
}
