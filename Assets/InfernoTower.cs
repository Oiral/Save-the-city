using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoTower : MonoBehaviour {

    public Block attachedBlock;

    private void Awake()
    {
        GameManager.instance.infernoTowers.Add(this);
        attachedBlock.infernoTower = this;
    }
}
