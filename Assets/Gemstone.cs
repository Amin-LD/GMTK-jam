using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gemstone : MonoBehaviour
{
    public bool canInteract;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.E))
        {
            transform.parent = player.transform.GetChild(0).transform;
            player.GetComponent<PlayerController>().triggerPanel.SetActive(false);
            player.GetComponent<PlayerController>().objectivePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Objective: \n Reach the green door";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
            player.GetComponent<PlayerController>().triggerPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = false;
            player.GetComponent<PlayerController>().triggerPanel.SetActive(false);
        }
    }
}
