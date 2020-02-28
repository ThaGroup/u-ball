using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Range(10,100)]
    public float Speed;
    public float JumpVelocity;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;

    public Text CountText;
    public Text WinText;

    public Camera[] Cameras;

    public bool IsSlowMotionEnabled = true;
    public float SlowDownMultiplier = 0.2f;
    private float _originalFixedDeltaTime;
    
    private Camera _currentCamera;
    private int _currentCameraId = 0;
    private Rigidbody _rb;
    private int _count = 0;

    void Start()
    {
        _originalFixedDeltaTime = Time.fixedDeltaTime;
        _currentCamera = Cameras[0];
        _currentCamera.enabled = true;
        _currentCamera.GetComponent<AudioListener>().enabled = true;
        _rb = GetComponent<Rigidbody>();
        _count = 0;
        SetCountText();
        WinText.text = string.Empty;
    }

    // TODO: Shift to boost.
    // TODO: Billards where you play as the cue ball.
    void FixedUpdate()
    {
        HandleCameraChange();

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var input = GetInput(moveHorizontal, moveVertical);

        float facing = _currentCamera.transform.eulerAngles.y;

        //Vector3 myInputs = ... (however you get X and Y into here);
        //// we rotate them around Y, assuming your inputs are in X and Z in the myInputs vector
        Vector3 myTurnedInputs = Quaternion.Euler(0, facing, 0) * input;

        _rb.AddForce(myTurnedInputs * Speed * GetShotBoost());
    }

    private Vector3 GetInput(float moveHorizontal, float moveVertical)
    {
        Vector3 input = new Vector3(moveHorizontal, 0, moveVertical);

        if (Input.GetKeyDown(KeyCode.Space) && _rb.velocity.y == 0)
        {
            input.y = JumpVelocity;
        }

        if (_rb.velocity.y < 0)
        {
            input += Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            input += Vector3.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }

        return input;
    }

    private static float GetShotBoost()
    {
        /**
         * When boosting, since the ball moves so fast
         * If the current camera is POV, maybe have an effect
         * Where the camera stays in place and the player watches the ball 
         * rocket forwards
         */
        var shotBoost = 1.0f;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            shotBoost = 150;
        }

        return shotBoost;
    }

    private void HandleCameraChange()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpdateCurrentCameraId();
            UpdateActiveCamera();
        }
    }

    private void UpdateActiveCamera()
    {
        Cameras.ToList().ForEach(c => {
            c.enabled = false;
            c.gameObject.GetComponent<AudioListener>().enabled = false;
        });

        _currentCamera = Cameras[_currentCameraId];
        _currentCamera.enabled = true;
        _currentCamera.gameObject.GetComponent<AudioListener>().enabled = true;
        Debug.Log("Active camera:");
        Debug.Log(_currentCamera.name);
    }

    private void UpdateCurrentCameraId()
    {
        _currentCameraId++;

        if (_currentCameraId >= Cameras.Length)
        {
            _currentCameraId = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            EnableSlowMotion();
            _rb.velocity = new Vector3(_rb.velocity.x / 2, _rb.velocity.y, _rb.velocity.z / 2);
        }
    }

    private void EnableSlowMotion()
    {
        if (IsSlowMotionEnabled)
        {
            Invoke(nameof(ResumeNormalTimeScale), 1.5f * SlowDownMultiplier);
            Time.timeScale = SlowDownMultiplier;
            //Time.fixedDeltaTime = Time.fixedDeltaTime * SlowDownMultiplier;
        }
    }

    private void ResumeNormalTimeScale()
    {
        Time.timeScale = 1.0f;
        //Time.fixedDeltaTime = _originalFixedDeltaTime;
        Debug.Log(Time.timeScale);
    }

    private void SetCountText()
    {
        CountText.text = "Count: " + _count.ToString();

        if (_count >= 10)
        {
            WinText.text = "You're Winner!";
        }
    }
}
