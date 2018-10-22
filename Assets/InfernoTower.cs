using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoTower : MonoBehaviour {

    public Block attachedBlock;

    public bool initialIgnition = true;

    private void OnEnable()
    {
        if (initialIgnition)
        {
            GameManager.instance.infernoTowers.Add(this);
        }
        attachedBlock.infernoTower = this;
    }
}
