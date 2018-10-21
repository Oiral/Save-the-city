using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClickScript : MonoBehaviour {

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, 100.0F);

            bool startHit = false;
            GameObject cornerHit = null;

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Renderer rend = hit.transform.GetComponent<Renderer>();
                if (hit.transform.tag == "Start")
                {
                    startHit = true;
                }else if (hit.transform.tag == "Corner")
                {
                    cornerHit = hit.transform.gameObject;
                }
            }

            if (startHit == true && cornerHit != null)
            {
                Debug.Log("Spawn");
            }
            else
            {
                Debug.Log("No spawn");
            }
        }
    }
}
