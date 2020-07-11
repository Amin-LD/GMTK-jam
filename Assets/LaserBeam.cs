using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private float camTime = 0f;
    private bool IsSpotted = false;
    public Material spotMaterial;
    public Material defaultMaterial;
    public Color spotColor;
    public Color defaultColor;
    public LayerMask layerMask;
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
            RaycastHit hit;
            transform.parent.GetChild(1).transform.LookAt(other.gameObject.transform);
            if (Physics.Raycast(transform.parent.GetChild(1).transform.position, transform.parent.GetChild(1).transform.forward, out hit,30f, layerMask, QueryTriggerInteraction.Collide))
            {
               if(hit.transform.gameObject.tag == "Player")
                {
                    IsSpotted = true;
                    transform.parent.GetComponent<SecurityCamera>().IsSpotted = true;
                    other.gameObject.GetComponent<PlayerController>().triggerPanel.SetActive(true);
                    gameObject.GetComponent<MeshRenderer>().material = spotMaterial;
                    transform.parent.GetChild(1).GetComponent<Light>().color = spotColor;
                }
            }

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
            transform.parent.GetComponent<SecurityCamera>().LeftRange();
            camTime = 0f;
            other.gameObject.GetComponent<PlayerController>().triggerPanel.SetActive(false);

        }
    }
}
