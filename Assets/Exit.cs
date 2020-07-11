using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool canInteract;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && player.GetComponent<PlayerController>().playerHasObject)
        {
            player.GetComponent<PlayerController>().objectivePanel.GetComponentInChildren<TextMeshProUGUI>().text = "YOU WIN";
            player.GetComponent<PlayerController>().triggerPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canInteract = true;
            player.GetComponent<PlayerController>().triggerPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'E' to escape";
            player.GetComponent<PlayerController>().triggerPanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canInteract = false;
            player.GetComponent<PlayerController>().triggerPanel.SetActive(false);
        }
    }
}
