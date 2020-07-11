using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public Vector3 from = new Vector3(30f, 0f, 90f);
    public Vector3 to = new Vector3(30f, -180f, 90f);
    public float speed = 1.0f;

    public bool IsSpotted = false;
    public float cameraWaitAfterLeaveRange = 5f;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!IsSpotted)
        {
            float t = Mathf.PingPong(Time.time * speed * 2.0f, 1.0f);
            transform.eulerAngles = Vector3.Lerp(from, to, t);
        }

        if (IsSpotted)
        {
            transform.LookAt(player.transform);
        }
    }

    public void LeftRange()
    {
        Debug.Log("leftRange");
        Invoke("LeftTimer", cameraWaitAfterLeaveRange);

    }
    public void LeftTimer()
    {
        Debug.Log("leftTimer");
        IsSpotted = false;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = transform.GetChild(0).gameObject.GetComponent<LaserBeam>().defaultMaterial;
        transform.GetChild(1).GetComponent<Light>().color = transform.GetChild(0).gameObject.GetComponent<LaserBeam>().defaultColor;
    }
}
