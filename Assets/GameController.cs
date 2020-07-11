using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject foodBar;

    public float maxTime = 100f;
    public float foodTimer;

    private void Awake()
    {
        foodTimer = maxTime;
    }

    private void Update()
    {
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        foodTimer -= Time.deltaTime;
        if (foodTimer > maxTime)
            foodTimer = maxTime;
        foodBar.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = foodTimer.ToString("0.00");
        foodBar.transform.GetChild(0).GetComponent<Slider>().value = foodTimer;
    }
}
