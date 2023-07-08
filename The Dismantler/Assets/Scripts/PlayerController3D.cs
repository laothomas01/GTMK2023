using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    RaycastHit lineOfSightHit_;
    Ray groundCheck_;
    RaycastHit groundCheckHit_;
    public bool DEBUG;
    Vector3 movement;
    Vector3 rotation;

    float forwardInput,rotationInput;
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
        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
        
        movement.Set(0f,0f,forwardInput * movementSpeed * Time.smoothDeltaTime);
        movement = transform.TransformDirection(movement); // set current controller's direction
        movement.y = rb.velocity.y; // Preserve the current vertical velocity
        rb.velocity = movement; //preserving the vertical velocity, including gravity

        lineOfSight_.origin = transform.position;
        lineOfSight_.direction = transform.forward;
        groundCheck_.origin = transform.position;
        groundCheck_.direction = Vector3.down;

        // Rotation controls
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        // Vector3 rotation = new Vector3(0f, rotationAmount, 0f);
        rotation.Set(0,rotationAmount,0);
        transform.Rotate(rotation);

        // Raycasting for line of sight
        if (Physics.Raycast(lineOfSight_, out lineOfSightHit_, raycastLookDistance_))
        {
            // Do something with the object hit by the raycast
            Debug.Log("Raycast hit: " + lineOfSightHit_.collider.gameObject.name);
        }
        


        if(isGrounded() && !isJumping)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
            Debug.Log("Jump");
            isJumping = true;
            }
          
        }
        
     
       
         if(DEBUG)
        {
               Debug.DrawRay(lineOfSight_.origin,lineOfSight_.direction * raycastLookDistance_,Color.red);

               Debug.DrawRay(groundCheck_.origin,groundCheck_.direction * raycastGroundCheckDistance_,Color.red);

         }
        
    }
    void FixedUpdate()
    {
        
        if(isJumping)
        {
             rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            
             isJumping = false;
        }
    }
    private bool isGrounded()
    {
        if (Physics.Raycast(groundCheck_, out groundCheckHit_, raycastGroundCheckDistance_))
        {
            // Do something with the object hit by the raycast
            Debug.Log("Raycast ground hit: " + groundCheckHit_.collider.gameObject.name);
            return true;
        }
  
        return false;
    }

}
