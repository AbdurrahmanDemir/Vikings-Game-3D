using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerAnimation playerAnimation;
    //[SerializeField] private SwordController sword;
    [Header("Settings")]
    float attackTimer;
    public int attackCount;
    float attackDelay=10f;
    public static bool canDealDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > attackDelay)
        {
            attackTimer = 0;
            attackCount = 0;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            attackTimer += Time.deltaTime;
            attackCount++;


            if (attackCount == 1)
            {
                playerAnimation.PlayAttack();
                //sword.Attack();
            }

        }

        
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        SwordController.hasDealtDamage.Clear();
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }
    public void ComboAttack1()
    {
        if (attackCount >= 2)
        {
            playerAnimation.PlayAttackCombo1();
            //sword.Attack();
        }
    }
    public void ComboAttack2()
    {
        if (attackCount >= 3)
        {
            playerAnimation.PlayAttackCombo2();
            //sword.Attack();
            attackCount = 0;
        }
    }

    public void AttackButton()
    {
        attackTimer += Time.deltaTime;
        attackCount++;


        if (attackCount == 1)
        {
            playerAnimation.PlayAttack();
            //sword.Attack();
        }

    }

}
