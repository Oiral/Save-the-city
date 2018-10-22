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
        Debug.Log("Corner", gameObject);
        GameManager.instance.CheckCorner(this);
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

    private void Update()
    {
        //Check if blocks on fire around
        
        if (connectedBlocks.Count > 0)
        {
            bool allConnectedBlockOnFire = true;

            foreach (Block block in connectedBlocks)
            {
                if (block.onFire == false)
                {
                    allConnectedBlockOnFire = false;
                }
            }
            if (allConnectedBlockOnFire)
            {
                //Check if connected corners all have fire connected to it
                bool allConnectedCornerOnFire = true;

                foreach (Corner corner in connectedCorners)
                {
                    bool cornerSafe = true;
                    foreach (Block conBlock in corner.connectedBlocks)
                    {
                        if (conBlock.onFire == true)
                        {
                            cornerSafe = false;
                            break;
                        }
                    }
                    if (cornerSafe == true)
                    {
                        allConnectedCornerOnFire = false;
                        break;
                    }
                }

                if (allConnectedCornerOnFire)
                {
                    movable = false;
                }
                else
                {
                    movable = true;
                }
            }
            else
            {
                movable = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Corner corner in connectedCorners)
        {
            Gizmos.DrawLine(transform.position, corner.transform.position);
        }
        
    }

    [ContextMenu("Remove Corner")]
    public void RemoveCorner()
    {
        foreach (Block block in connectedBlocks)
        {
            block.connectedCorners.Remove(this);
            if (block.connectedCorners.Count == 0)
            {
                block.RemoveBlock();
            }
        }
        foreach (Corner corner in connectedCorners)
        {
            corner.connectedCorners.Remove(this);
            if (corner.connectedCorners.Count == 0)
            {
                corner.RemoveCorner();
            }
        }
        
        DestroyImmediate(gameObject);
    }
}
