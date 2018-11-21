using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerUIScript : MonoBehaviour {

    public GameObject smallUI;
    public GameObject bigUI;

	public void OnHover()
    {
        smallUI.SetActive(false);
        bigUI.SetActive(true);
    }
    public void LeaveHover()
    {
        smallUI.SetActive(true);
        bigUI.SetActive(false);
    }
}
