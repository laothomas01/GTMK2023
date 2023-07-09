using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//@TODO adjust the controls to not be stuck to the keyboard becuase that gives a shitty 1st impression
public class PlayerController3D : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;

    public float raycastLookDistance_ = 10f;
    public float raycastGroundCheckDistance_ = 0.2f;

    public bool isJumping;
    public float jumpForce = 5f;
    private Rigidbody rb;
    public int gravityScale;


    Ray lineOfSight_;
    Ray groundCheck_;
    public bool DEBUG;
    Vector3 movement;
    Vector3 rotation;

    float forwardInput, rotationInput;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityScale;
        lineOfSight_ = new Ray();
        groundCheck_ = new Ray();
        DEBUG = true;
        isJumping = false;
        movement = new Vector3();
        rotation = new Vector3();
    }

    void Update()
    {
        // // Movement controls
        //@TODO turn into mouse control and scaled by sensitivity

        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        movement.Set(0f, 0f, forwardInput * movementSpeed * Time.smoothDeltaTime);
        movement = transform.TransformDirection(movement); // set current controller's direction
        movement.y = rb.velocity.y; // Preserve the current vertical velocity
        rb.velocity = movement; //preserving the vertical velocity, including gravity

        lineOfSight_.origin = transform.position;
        lineOfSight_.direction = transform.forward;



        // Rotation controls
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        // Vector3 rotation = new Vector3(0f, rotationAmount, 0f);
        rotation.Set(0, rotationAmount, 0);
        transform.Rotate(rotation);





        groundCheck_.origin = transform.position;
        groundCheck_.direction = Vector3.down;

        if (isGrounded() && !isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Debug.Log("Jump");
                isJumping = true;
            }

        }

        if(Input.GetKey(KeyCode.LeftControl) ||Input.GetKey(KeyCode.RightControl) )
        {
            Dismantle();
        }



        if (DEBUG)
        {
            Debug.DrawRay(lineOfSight_.origin, lineOfSight_.direction * raycastLookDistance_, Color.red);

            Debug.DrawRay(groundCheck_.origin, groundCheck_.direction * raycastGroundCheckDistance_, Color.red);

        }

    }
    void FixedUpdate()
    {

        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);

            isJumping = false;
        }
    }
    private bool isGrounded()
    {
        if (Physics.Raycast(groundCheck_, out RaycastHit groundCheckHit, raycastGroundCheckDistance_))
        {
            // Do something with the object hit by the raycast
            // Debug.Log("Raycast ground hit: " + groundCheckHit.collider.gameObject.name);
            return true;
        }

        return false;
    }

    // an attack function
    // 
    public void Dismantle()
    {

        // Raycasting for line of sight
        if (Physics.Raycast(lineOfSight_, out RaycastHit lineOfSightHit, raycastLookDistance_))
        {
            // Check if the hit object has a Targettable component
            Targettable target = lineOfSightHit.collider.GetComponent<Targettable>();
            if (target != null)
            {
                target.OnHit();
            }
        }

    }
    public void Transmute()
    {

    }
    
    public void GravityShift()
    {

    }
    


}
