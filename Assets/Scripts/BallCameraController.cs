using UnityEngine;

public class BallCameraController : MonoBehaviour
{
    public GameObject Ball;

    public float OffsetX = 0.0f;
    public float OffsetY = 0.0f;
    public float OffsetZ = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(Ball.transform.position, Vector3.up, Input.GetAxis("Mouse X"));
        transform.RotateAround(Ball.transform.position, Vector3.left, Input.GetAxis("Mouse Y"));
        transform.position = GetNewPosition();
        transform.LookAt(Ball.transform);
    }

    private Vector3 GetNewPosition()
    {
        var targetPosition = Ball.transform.position;

        var newPositionY = targetPosition.y + OffsetY;
        var newPositionZ = targetPosition.z + OffsetZ;

        return new Vector3(transform.position.x, newPositionY, newPositionZ);
    }

    private Quaternion GetNewRotation()
    {
        var rotation = transform.rotation;
        var targetRotation = Ball.transform.rotation;

        var newRotationX = targetRotation.x;
        var newRotationY = targetRotation.y;
        var newRotationZ = targetRotation.z;

        return new Quaternion(newRotationX, newRotationY, newRotationZ, rotation.w);
    }
}
