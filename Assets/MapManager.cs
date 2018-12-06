using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	public List<MapMarker> markers = new List<MapMarker>();

    public GameObject mainCamera;
    public int targetMarker = 0;
    public float moveSpeed = 1f;
    public Vector3 offset = new Vector3(0,15,0);
    
    private void Update()
    {
        Vector3 pos = Vector3.Lerp(mainCamera.transform.position, markers[targetMarker].transform.position + offset, Time.deltaTime * moveSpeed);
        mainCamera.transform.position = pos;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetMarker(targetMarker += 1);
        }
    }

    public void SetMarker(int marker)
    {
        targetMarker = marker;
    }
    public void SetMarker(MapMarker marker)
    {
        targetMarker = markers.IndexOf(marker);
    }

    public void CallMarker()
    {
        markers[targetMarker].markerEvent.Invoke();
    }
    public void CallMarker(int marker)
    {
        markers[marker].markerEvent.Invoke();
    }
}
