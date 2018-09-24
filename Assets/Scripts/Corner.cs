using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {

    public List<Block> connectedBlocks;

    public List<Corner> connectedCorner;

    public bool blocked = false;

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().MovePlayer(this);
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
}
