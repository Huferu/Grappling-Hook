using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public float runSpeed;

    public Vector3 jump;
    public float jumpForce = 2.0f;
   

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    [SerializeField] private float groundDrag;

    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        SpeedControl();

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(Application.loadedLevel);

        if (Input.GetKeyDown(KeyCode.Escape)) {  
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Run();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new (rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x,rb.velocity.y,limitVel.z);
        }

        
    }

    private void Run()
    {
       
    }

    private void Jump()
    {

        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }    
}
