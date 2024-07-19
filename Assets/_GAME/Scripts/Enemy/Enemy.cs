using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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
        animator.SetTrigger("damage");
        health -= damage;
        if (health < 0)
            Die();
    }
    void Die()
    {
        Debug.Log("enemy öldü");
        Destroy(gameObject);
    }
}
