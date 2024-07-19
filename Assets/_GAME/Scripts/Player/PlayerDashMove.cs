using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashMove : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private Animator animator;
    public static bool dashing;

    public float dashSpeed;
    public float dashTime;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            dashing = true;
            animator.Play("Dash");
            controller.characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return new WaitForSeconds(1f);
            dashing = false;
        }
    }

}
