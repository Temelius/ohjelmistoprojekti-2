using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class checkpointMessage : MonoBehaviour
{
    public GameObject GuidePanel;
    public GameObject Trigger;
    public TMP_Text GuideText;

    public string guideMessage;
    public float messageDelay;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ShowMessage(guideMessage, messageDelay));
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        GuidePanel.SetActive(true);
        GuideText.text = message;
        yield return new WaitForSeconds(delay);
        GuidePanel.SetActive(false);
    }
}
