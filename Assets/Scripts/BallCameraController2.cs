using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraController2 : MonoBehaviour
{
    public GameObject Ball;
    public float Close = 1f;
    public float Normal = 25f;
    public float Far = 50.0f;

    public string CameraDistance = "Close";

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Ball.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
