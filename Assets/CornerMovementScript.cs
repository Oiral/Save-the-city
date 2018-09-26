using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornerMovementScript : MonoBehaviour {

    public Corner currentCorner;
    public float moveSpeed = 10;

    public int actionPoints = 3;

    public int maxActionPoints = 3;

    public Text actionCounter;

    private void Start()
    {
        if (currentCorner == null)
        {
            //Find a new corner and set it to the first one
            currentCorner = GameObject.FindGameObjectWithTag("Corner").GetComponent<Corner>();
        }
    }

    public void NextCorner(Corner cornerToMove)
    {
        //check if we can move
        if (cornerToMove.blocked == true && actionPoints >= 2)
        {
            cornerToMove.UnBlockCorner();
            actionPoints -= 2;
        } else if (currentCorner.connectedCorner.Contains(cornerToMove) && actionPoints > 0 && cornerToMove.blocked == false)
        {
            currentCorner = cornerToMove;
            actionPoints -= 1;
        }
    }

    private void OnMouseDown()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.SelectPlayer(this);
    }

    private void Update()
    {
        if (currentCorner!= null)
        {
            transform.position = Vector3.Lerp(transform.position, currentCorner.gameObject.transform.position, moveSpeed * Time.deltaTime);
        }
        actionCounter.text = actionPoints.ToString();
    }

    public void ResetActionPoints()
    {
        actionPoints = maxActionPoints;
    }

    public bool CanRemoveFire(Block blockClicked)
    {
        if (currentCorner.connectedBlocks.Contains(blockClicked) && actionPoints >= 2 && blockClicked.onFire)
        {
            actionPoints -= 2;
            return true;
        }
        else
        {
            return false;
        }
    }

}
