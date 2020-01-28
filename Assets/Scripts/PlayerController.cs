using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Range(10,20)]
    public float Speed;
    public float JumpVelocity;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;

    public Text CountText;
    public Text WinText;

    public GameObject Camera;
    
    private Rigidbody _rb;
    private int _count = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _count = 0;
        SetCountText();
        WinText.text = string.Empty;
    }

    // TODO: Shift to boost.
    // TODO: Billards where you play as the cue ball.
    void Update()
    {        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
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

        float facing = Camera.transform.eulerAngles.y;

        //Vector3 myInputs = ... (however you get X and Y into here);
        //// we rotate them around Y, assuming your inputs are in X and Z in the myInputs vector
        Vector3 myTurnedInputs = Quaternion.Euler(0, facing, 0) * input;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.AddForce(myTurnedInputs * Speed);       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            _count++;
            SetCountText();
        }
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
