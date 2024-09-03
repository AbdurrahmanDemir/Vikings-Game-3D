using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthSlider;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Start()
    {
        health = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("damage");
        health -= damage;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        if (health < 0)
            Die();
    }
    void Die()
    {
        Debug.Log("enemy öldü");
        Destroy(gameObject);

        gameManager.GameWin();
    }
}
