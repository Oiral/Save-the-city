using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region singleton
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this);
        }
    }

    #endregion

    public GameObject squadSelectionPanel;
    public GameObject startLevelButton;

    [Header("Notifation Bar")]
    public GameObject notificationPanel;
    public Text notificationText;

    public void Notification(string text)
    {
        notificationText.text = text;
        StartCoroutine(RunNotificaiton());
    }

    IEnumerator RunNotificaiton()
    {
        yield return new WaitForSeconds(0.1f);
    }

}
