using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    //[Header("Elements")]
    //[SerializeField] private Transform hitDetectionTransform;
    //[SerializeField] private Collider hitCollider;
    //[SerializeField] private Transform hitDetectionRadius;
    //[SerializeField] private LayerMask enemyMask;
    //[SerializeField] private LayerMask playerMask;

    [Header("Settings")]
    public static List<GameObject> hasDealtDamage;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] float weaponLength;
    [SerializeField] int weaponDamage;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAttack.canDealDamage = false;
        hasDealtDamage= new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAttack.canDealDamage)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, -transform.up * weaponLength, Color.red);
            if (Physics.Raycast(transform.position,-transform.up,out hit, weaponLength, enemyMask))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy)&& !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    Debug.Log("enemy hasar yedi");
                    enemy.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.Log("Raycast didn't hit anything.");
            }
        }
    }


    //public void Attack()
    //{



    //    //Collider[] enemies = Physics.OverlapBox(hitDetectionTransform.position, hitDetectionRadius.localScale, hitDetectionRadius.localRotation, enemyMask);

    //    //for (int i = 0; i < enemies.Length; i++)
    //    //{
    //    //    Debug.Log("enemy hasar yedi");
    //    //}
    //    //Collider[] player = Physics.OverlapBox(hitDetectionTransform.position, hitDetectionRadius.localScale, hitDetectionRadius.localRotation, playerMask);

    //    //for (int i = 0; i < player.Length; i++)
    //    //{
    //    //    Debug.Log("player hasar yedi");
    //    //}


    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    //}
}
