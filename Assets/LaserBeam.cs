using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private float camTime = 0f;
    private bool IsSpotted = false;
    private void Update()
    {
        if (IsSpotted)
        {
            camTime += Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsSpotted = true;
            other.gameObject.GetComponent<PlayerController>().triggerPanel.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.gameObject.GetComponent<PlayerController>().triggerPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Camera time: " + camTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsSpotted = false;
            camTime = 0f;
            other.gameObject.GetComponent<PlayerController>().triggerPanel.SetActive(false);
        }
    }
}
