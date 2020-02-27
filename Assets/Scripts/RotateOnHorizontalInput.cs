using UnityEngine;

public class RotateOnHorizontalInput : MonoBehaviour
{
    public float magnitude = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, magnitude * Input.GetAxis("Horizontal"));
    }
}
