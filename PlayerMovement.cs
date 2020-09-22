using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{


    public float walkingSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpSpeed = 2f;
    public float runningSpeed = 24f;
    Vector3 velocity;
    Vector3 originalPosition;
    CharacterController player;

    void Start()
    {
        player = GetComponent<CharacterController>();
        originalPosition = transform.position;
    }

    bool tooSteep(float p, float a)
    {
        return a - p < 1 ? true : false;
    }

    bool outsideScene()
    {
        return transform.position.y < 0 ? true : false;
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");
        float speed;
        
        if (player.isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        if (Input.GetButton("Jump") && player.isGrounded)
        {
            velocity.y = jumpSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey("joystick button 0"))
        {
            speed = runningSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }

        velocity = transform.right * velocity.x * speed + transform.forward * velocity.z * speed + transform.up * velocity.y;

        player.Move(velocity * Time.deltaTime);

        if (outsideScene())
        {
            transform.position = originalPosition;
        }

    }
}