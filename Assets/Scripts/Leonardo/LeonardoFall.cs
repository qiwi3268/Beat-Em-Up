using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonardoFall : MonoBehaviour
{
    public static bool tookDamage;
    public static bool knockedDown;
    private const float stunTime = 0.2f;
    private const float knockedDownTime = 2;
    private const float riseTime = 0.5f;
    private bool rise;
    private Stats leoState;
    private Rigidbody rigidBody;
    private Animator animator;

    private void Awake()
    {    
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        leoState = GetComponent<Stats>();
        leoState.health = 25;
    }

    private void FixedUpdate()
    {
        if (knockedDown && LeonardoMovement.canMove)
            StartCoroutine(KnockedDown());
        if (tookDamage && !knockedDown)
        {
            if (leoState.health <= 0)
                StartCoroutine(KnockedDown());
            else
                StartCoroutine(TookDamage());
        }

        if (knockedDown && Input.anyKeyDown && rise)
            StartCoroutine(Rise());
    }

    private IEnumerator TookDamage()
    {
        animator.Play("Hurt");
        LeonardoMovement.canMove = false;
        yield return new WaitForSeconds(stunTime);
        LeonardoMovement.canMove = true;
        tookDamage = false;
    }

    private IEnumerator KnockedDown()
    {
        animator.Play("Fall");
        animator.SetBool("Knocked Down", true);
        LeonardoMovement.canMove = false;
        yield return new WaitForSeconds(knockedDownTime);
        if (leoState.health <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.GameOver();
        }
        animator.SetBool("Knocked Down", false);
        animator.SetBool("Rise", true);
        rise = true;
    }

    private IEnumerator Rise()
    {
        animator.Play("Rise");
        yield return new WaitForSeconds(riseTime);
        knockedDown = false;
        animator.SetBool("Rise", false);
        LeonardoMovement.canMove = true;
        rise = false;
    }
}
