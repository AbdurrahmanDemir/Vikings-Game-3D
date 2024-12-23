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
            BlockAnimation(); // Blok yapılıyor
            Debug.Log("çalıştı");

        }
        else
        {
            animator.SetLayerWeight(2, 0); // Blok animasyonu kapatılıyor
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
        isBlocking = true; // Butona basıldığı anda blok başlatılıyor
        Debug.Log("Butona basılıyor");
    }
    public void BlockButtonExit()
    {
        isBlocking = false; // Buton bırakıldığında blok duruyor
        Debug.Log("Butonu bırakıldı");

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
        UIManager.instance.ShowCombatMultiplier(2);
    }
    public void PlayAttackCombo2()
    {
        animator.SetLayerWeight(1, 1);
        animator.Play("Attack3");
        UIManager.instance.ShowCombatMultiplier(3);
    }

}
