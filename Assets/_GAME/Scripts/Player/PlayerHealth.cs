using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Animator animator;
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthSlider;


    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
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
