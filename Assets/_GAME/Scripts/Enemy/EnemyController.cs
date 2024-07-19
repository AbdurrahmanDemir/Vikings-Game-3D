using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState 
{
    Idle,
    Run,
    Attack

}
public class EnemyController : MonoBehaviour
{
    [Header("Settings")]
    EnemyState enemyState;
    [SerializeField] private int moveSpeed;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
    public static bool canDealDamage;

    [Header("Agent")]
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;

    void Start() 
    {
        player = GameObject.FindWithTag("Player");
        agent= GetComponent<NavMeshAgent>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed+=Time.deltaTime;

        if (newDestinationCD <= 0&& Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);

    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        EnemySwordController.hasDealtDamage = false;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }
    public void StopAttack()
    {
        enemyState = EnemyState.Idle;
        animator.SetLayerWeight(1, 0);
        animator.StopPlayback();
    }



    public void TryAttack()
    {
        animator.SetLayerWeight(1, 1);
        animator.Play("EnemyAttack1");
    }
}
