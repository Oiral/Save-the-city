using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour {

    public Block attachedBlock;

    private void Start()
    {
        attachedBlock.attachedPump = this;
        LevelManager.instance.pumps.Add(this);
    }
}
