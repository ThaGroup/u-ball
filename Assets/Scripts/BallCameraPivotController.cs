using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraPivotController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y"));
    }
}
