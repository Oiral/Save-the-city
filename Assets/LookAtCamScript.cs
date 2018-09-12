using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamScript : MonoBehaviour {

    GameObject cam;

    private void Start()
    {
        cam = Camera.main.gameObject;
    }

    private void Update()
    {
        transform.LookAt(cam.transform.position, Vector3.up);
        transform.Rotate(new Vector3(0, 180));
    }
}
