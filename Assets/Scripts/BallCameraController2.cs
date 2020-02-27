using UnityEngine;

public class BallCameraController2 : MonoBehaviour
{
    public GameObject Ball;

    /**
     * Ideally there would be preset values that
     * allow you to adjust the camera to preset positions/heights
     * But it being the child of camera pivot complicates matteres
     * So all this script really does for now is start the camera looking at the ball
     * Then the parent (camera pivot) keeps it in line from there
     */

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
