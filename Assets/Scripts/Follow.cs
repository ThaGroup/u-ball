using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public bool FollowPositionX = true;
    public bool FollowPositionY = true;
    public bool FollowPositionZ = true;
    
    public bool FollowRotationX = true;
    public bool FollowRotationY = true;
    public bool FollowRotationZ = true;

    public float OffsetX = 0.0f;
    public float OffsetY = 0.0f;
    public float OffsetZ = 0.0f;

    public float OrbitX = 0.0f;
    public float OrbitY = 0.0f;
    public float OrbitZ = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = GetNewPosition();
        //transform.rotation = GetNewRotation();
        //transform.LookAt(Target.transform);
    }

    private Vector3 GetNewPosition()
    {
        var position = transform.position;
        var targetPosition = Target.transform.position;

        var newPositionX = FollowPositionX ? targetPosition.x + OffsetX : position.x + OffsetX;
        var newPositionY = FollowPositionY ? targetPosition.y + OffsetY : position.y + OffsetY;
        var newPositionZ = FollowPositionZ ? targetPosition.z + OffsetZ : position.z + OffsetZ;

        return new Vector3(newPositionX, newPositionY, newPositionZ);
    }

    private Quaternion GetNewRotation()
    {
        var rotation = transform.rotation;
        var targetRotation = Target.transform.rotation;

        var newRotationX = FollowRotationX ? targetRotation.x : rotation.x;
        var newRotationY = FollowRotationY ? targetRotation.y : rotation.y;
        var newRotationZ = FollowRotationZ ? targetRotation.z : rotation.z;

        return new Quaternion(newRotationX, newRotationY, newRotationZ, rotation.w);
    }
}
