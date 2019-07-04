using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonardoJump : MonoBehaviour
{
    private const float runJumpTime = 0.667f;
    private const float jumpVelocity = 3;
    private const float runJumpForce = 40;
    private const float fallMultiplier = 7;
    private bool isGrounded;
    private bool isFalling;
    private float jumpTimeCounter;
    private float jumpForce;
    private Animator animator;
    private Rigidbody rigidBody;
    private LeonardoState leoState;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        leoState = GetComponent<LeonardoState>();
        isGrounded = true;
        isFalling = false;
        jumpTimeCounter = 0;       
    }

    private void FixedUpdate()
    {       
        if (Input.GetKey(KeyCode.Space) || isFalling && LeonardoMovement.canMove)
        {
            if (leoState.currentState != leoState.runState 
                && leoState.currentState != leoState.runJumpState)
                Jump();
            else if (leoState.currentState != leoState.runJumpState)
                StartCoroutine(RunJump());
        }
    }

    private IEnumerator RunJump()
    {
        animator.Play("RunJump");
        LeonardoMovement.walkTimeCounter = 0;
        rigidBody.AddForce(transform.right * (LeonardoMovement.facingRight ? 1 : -1) * runJumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(runJumpTime);
        animator.Play("Idle");
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter < 0.4)
                jumpTimeCounter += Time.deltaTime;
            if (!isFalling)
            {
                animator.SetBool("Is Jump", true);
                isGrounded = false;
            }
        }

        jumpForce += Time.deltaTime;
        jumpTimeCounter -= Time.deltaTime;
        isFalling = transform.position.y > 0.19f;

        if (Input.GetKeyUp(KeyCode.Space))
            jumpTimeCounter = 0.25f;

        if ((Input.GetKey(KeyCode.Space) || jumpTimeCounter > 0) && jumpForce < 0.65 
            && leoState.currentState != leoState.runState)
            rigidBody.velocity = Vector3.up * jumpVelocity;

        if (isFalling)
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else
        {
            jumpForce = 0;
            jumpTimeCounter = 0;
            animator.SetBool("Is Jump", false);
            isGrounded = true;
        }
    }
}
