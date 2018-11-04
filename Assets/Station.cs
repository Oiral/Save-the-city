using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {

    private void Start()
    {
        PlayerMovementScript movementScript = GetComponent<PlayerMovementScript>();
        GameManager.instance.station.SetPlayer(movementScript);
    }
}
