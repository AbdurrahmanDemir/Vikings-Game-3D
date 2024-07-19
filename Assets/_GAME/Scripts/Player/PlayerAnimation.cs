using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [Header("Settings")]
    [SerializeField] private float moveSpeedMultiplier;
    public static bool canBlock;
    public void ManageAnimations(Vector3 moveVector)
    {
        if (moveVector.magnitude > 0 && !PlayerDashMove.dashing)
        {
            animator.SetFloat("moveSpeed", moveVector.magnitude * moveSpeedMultiplier);
            playRunAnimation();

            animator.transform.forward = moveVector.normalized;
        }
        else
        {
            playIdleAnimation();
        }
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.F))
        {
            BlockAnimation();
        }
        else
        {
            animator.SetLayerWeight(2, 0);
            animator.StopPlayback();
            canBlock = false;
        }
    }
    public void BlockAnimation()
    {
        canBlock = true;
        animator.SetLayerWeight(2, 1);
        animator.Play("Block");
    }

    public void playRunAnimation()
    {
        animator.Play("Run");
    }
    public void playIdleAnimation()
    {
        animator.Play("Idle");
    }
    public void PlayAttack()
    {
        animator.SetLayerWeight(1, 1);
        animator.Play("Attack1");
    }
    public void PlayAttackCombo1()
    {
        animator.SetLayerWeight(1, 1);
        animator.Play("Attack2");
    }
    public void PlayAttackCombo2()
    {
        animator.SetLayerWeight(1, 1);
        animator.Play("Attack3");
    }

}
