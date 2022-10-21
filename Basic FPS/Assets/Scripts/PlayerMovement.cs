using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;
    public float playerHeight;
    public float groundDrag;
    public LayerMask Ground;
    public float jumpForce;
    public float airMultiplier;
    bool grounded;
    float horizontalInput;
    float verticalInput;
    bool readyToJump = true;
    Vector3 moveDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation=true;
    }

    // Update runs once per frame. FixedUpdate can run once, zero, or several times per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        PlayerInput();
        SpeedCont();
        if(grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
    }
    private void FixedUpdate() {
        PlayerMove();
    }
    public void PlayerInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.Space) && grounded && readyToJump == true){
            readyToJump = false;
            Jumping();
        }
        else if(!grounded){
            ResetJump();
        }
    }
    public void PlayerMove(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed* 10f, ForceMode.Force);
        }
        else if(!grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed* 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedCont(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude>moveSpeed){
            Vector3 limitedVel = flatVel.normalized*moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    public void Jumping(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump(){
        readyToJump = true;
    }
}
