using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [Header("Settings")]
    [SerializeField] private float moveSpeedMultiplier;
    public static bool canBlock;
    private bool isBlocking = false;

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
        if (isBlocking)
        {
            BlockAnimation(); // Blok yap�l�yor
            Debug.Log("�al��t�");

        }
        else
        {
            animator.SetLayerWeight(2, 0); // Blok animasyonu kapat�l�yor
            animator.StopPlayback();
            canBlock = false;
        }

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

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    JumpAnimation();
        //}
        //else
        //{
        //    animator.SetLayerWeight(3, 0);
        //    animator.StopPlayback();
        //}
    }

    public void BlockButton()
    {
        isBlocking = true; // Butona bas�ld��� anda blok ba�lat�l�yor
        Debug.Log("Butona bas�l�yor");
    }
    public void BlockButtonExit()
    {
        isBlocking = false; // Buton b�rak�ld���nda blok duruyor
        Debug.Log("Butonu b�rak�ld�");

    }
    public void BlockAnimation()
    {
        canBlock = true;
        animator.SetLayerWeight(2, 1);
        animator.Play("Block");
    }

    public void JumpAnimation()
    {
        animator.SetLayerWeight(3, 1);
        animator.Play("Jump");
    }

    public void playRunAnimation()
    {
        animator.Play("Run");
    }
    public void playIdleAnimation()
    {
        animator.Play("Idle");
        animator.SetFloat("Attack1", 0f);
       
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
