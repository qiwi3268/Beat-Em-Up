using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonardoMovement : MonoBehaviour
{
    public static bool facingRight;
    public static bool canMove;
    public static float walkTimeCounter;
    private const int walkMovementSpeed = 1;
    private const int xMax = 1000;
    private const int xMin = -1000;
    private const int zMin = -5;
    private const int zMax = 5;
    private float movementSpeed;
    private Rigidbody rigidBody;
    private Animator animator;
    private LeonardoState leoState;

    private void Awake()
    {
        GetComponent<LeonardoJump>().enabled = true;
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        leoState = GetComponent<LeonardoState>();
        movementSpeed = walkMovementSpeed;
        facingRight = true;
        canMove = true;      
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBody.velocity = movement * movementSpeed;
        rigidBody.position = new Vector3
                                    (
                                        Mathf.Clamp(rigidBody.position.x, xMin, xMax),
                                        transform.position.y,
                                        Mathf.Clamp(rigidBody.position.z, zMin, zMax)
                                    );

        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();

        animator.SetBool("Moving Away", moveVertical > 0);
        if (moveHorizontal == 0)
            rigidBody.velocity *= 1.5f;

        animator.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);

        if (Input.GetKey(KeyCode.S))
            animator.SetBool("Moving Away", false);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            walkTimeCounter += Time.deltaTime;
            if (walkTimeCounter > 0.8)
            {
                animator.SetBool("Is Run", true);
                rigidBody.velocity *= 1.75f;
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            walkTimeCounter = 0;
            animator.SetBool("Is Run", false);
        }
        
        if (leoState.currentState == leoState.attack1State 
            || leoState.currentState == leoState.attack2State 
            || leoState.currentState == leoState.attack3State || !canMove)
            movementSpeed = 0;
        else if (leoState.currentState == leoState.runAttackState)
            movementSpeed = 0;
        else
            movementSpeed = walkMovementSpeed;
    }

    private void Flip()
    {
        if (canMove)
        {
            facingRight = !facingRight;
            Vector3 thisScale = transform.localScale;
            thisScale.x *= -1;
            transform.localScale = thisScale;
        }
    }

}

