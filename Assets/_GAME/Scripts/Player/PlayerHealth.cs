using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Animator animator;
    private int health;
    [SerializeField] private int maxHealth;


    private void Start()
    {
        health = maxHealth;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            Die();
    }
    public void Block()
    {
        animator.SetLayerWeight(2, 1);
        animator.SetTrigger("BlockHit");
    }
    void Die()
    {
        Debug.Log("enemy öldü");
        Destroy(gameObject);
    }
}
