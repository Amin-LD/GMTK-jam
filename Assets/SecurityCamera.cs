using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float minAngle = 0;
    public float maxAngle = 180;
    private float startTime;
    public Vector3 from = new Vector3(30f, 0f, 90f);
    public Vector3 to = new Vector3(30f, -180f, 90f);
    public float speed = 1.0f;

    private void Start()
    {
        startTime = Time.deltaTime;
    }

    private void Update()
    {
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(minAngle, maxAngle, Mathf.Sin(Time.deltaTime - startTime)), transform.eulerAngles.z);


        float t = Mathf.PingPong(Time.time * speed * 2.0f, 1.0f);
        transform.eulerAngles = Vector3.Lerp(from, to, t);
    }

}
