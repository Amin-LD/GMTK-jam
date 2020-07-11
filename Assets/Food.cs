using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float timeToAdd = 10f;
    public string foodName = "Apple";
    [HideInInspector]
    public bool canInteract;
    private GameObject player;

    private GameController gc;
    private void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.E))
        {
            gc.foodTimer += timeToAdd;
            player.GetComponent<PlayerController>().triggerPanel.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canInteract = true;
            player.GetComponent<PlayerController>().triggerPanel.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Press 'E' to eat: " + foodName;
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
