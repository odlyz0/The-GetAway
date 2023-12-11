using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float currentSpeed;
    [SerializeField] public float walkingSpeed = 5f;
    [SerializeField] public float runningSpeed = 7f;

    private Vector3 moveDirection;
    private Vector3 moveDirectionZ;
    private Vector3 moveDirectionX;
    private Vector3 velocity;

    public float gravity = -9.81f;
    public float jumpHeigt = 2f;

    private CharacterController characterController;

    private float baselineGravity;
    private float xMove;
    private float zMove;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");


        moveDirectionZ = new Vector3(0, 0, moveZ);
        moveDirectionX = new Vector3(moveX, 0, 0);
        moveDirection = transform.TransformDirection(moveDirectionZ + moveDirectionX);




        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }

        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

            if (moveDirection != Vector3.zero)
            {
                Idle();
            }
        }

        characterController.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime; //applies gravity
        characterController.Move(velocity * Time.deltaTime);

    }
    private void Walk()
    {
        moveDirection *= walkingSpeed;
    }
    private void Run()
    {
        moveDirection *= runningSpeed;

    }
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeigt * -2 * gravity);
    }
    private void Idle() { }

}



