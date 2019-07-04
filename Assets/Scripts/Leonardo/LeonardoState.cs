using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonardoState : MonoBehaviour
{
    public int currentState;
    public int idleState = Animator.StringToHash("Base Layer.Idle");
    public int walk1State = Animator.StringToHash("Base Layer.Walk1");
    public int walk2State = Animator.StringToHash("Base Layer.Walk2");
    public int jumpState = Animator.StringToHash("Base Layer.Jump");
    public int attack1State = Animator.StringToHash("Base Layer.Attack1");
    public int attack2State = Animator.StringToHash("Base Layer.Attack2");
    public int attack3State = Animator.StringToHash("Base Layer.Attack3");
    public int runState = Animator.StringToHash("Base Layer.Run");
    public int runAttackState = Animator.StringToHash("Base Layer.RunAttack");
    public int runJumpState = Animator.StringToHash("Base Layer.RunJump");
    public int fallState = Animator.StringToHash("Base Layer.Fall");
    private AnimatorStateInfo currentStateInfo;
    private Animator animator; 

    private void Awake()
    {   
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;
        DebugLog();
    }

    private void DebugLog()
    {
        if (currentState == idleState)
            Debug.Log(idleState);
        if (currentState == walk1State)
            Debug.Log(walk1State);
        if (currentState == walk2State)
            Debug.Log(walk2State);
        if (currentState == attack1State)
            Debug.Log(attack1State);
        if (currentState == runState)
            Debug.Log(runState);
        if (currentState == runAttackState)
            Debug.Log(runAttackState);
    }
}
