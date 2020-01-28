using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float CameraRotationSmoothingCoefficient; // A bigger number makes the camera more jittery but more responsive.
    public float CameraPullawayCoefficient;

    private Rigidbody _playerRb;
    private Vector3 _offset;
    

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = Player.GetComponent<Rigidbody>();
        _offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var lookRotation = Quaternion.LookRotation(Player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * CameraRotationSmoothingCoefficient);
        //transform.LookAt(Player.transform.position, Vector3.left);
        
        if (_playerRb.velocity.magnitude != 0)
        {
            var playerVelocity = _playerRb.velocity;

            // Since you currently can't move in the y plane, set the y component to the negative of the 
            // the initial y offset so that the camera will remain slightly above the ball.
            playerVelocity.y = (-1) * _offset.y;
            transform.position = Player.transform.position - Vector3.Scale(playerVelocity, new Vector3(CameraPullawayCoefficient, CameraPullawayCoefficient, CameraPullawayCoefficient));
        }
    }

    //Vector3 GetBehindPosition(Transform target, float distanceBehind, float distanceAbove)
    //{
    //    return target.position - (target.forward * distanceBehind) + (target.up * distanceAbove*0);
    //}

    //float facing = Camera.main.transform.eulerAngles.y

    //Vector3 myInputs = ... (however you get X and Y into here);
    //// we rotate them around Z, assuming your inputs are in X and Y in the myInputs vector
    //Vector3 myTurnedInputs = Quaternion.Euler(0, 0, facing) * myInput;

}
