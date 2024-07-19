using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAttack playerAttack;

    public void StopAttack()
    {
        animator.SetLayerWeight(1, 0);
        animator.StopPlayback();
        PlayerAttack.canDealDamage = false;
        playerAttack.attackCount = 0;
    }
}
