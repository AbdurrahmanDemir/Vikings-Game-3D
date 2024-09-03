using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [Header("Settings")]
    public static List<GameObject> hasDealtDamage;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] float weaponLength;
    [SerializeField] int weaponDamage;
    [SerializeField] private GameObject damageParticle;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAttack.canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(PlayerAttack.canDealDamage);

        if (PlayerAttack.canDealDamage)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, -transform.up * weaponLength, Color.green);
            if (Physics.Raycast(transform.position,-transform.up,out hit, weaponLength, enemyMask))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    Debug.Log("enemy hasar yedi");
                    enemy.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                    DamageParticle(hit.point);
                }
            }
            else
            {
                Debug.Log("Raycast didn't hit anything.");
            }
        }
    }

    public void DamageParticle(Vector3 pos)
    {
        GameObject particle = Instantiate(damageParticle, pos, Quaternion.identity);
        Destroy(particle, 3f);
    }
    public void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, -transform.up * weaponLength, Color.green);

    }
}
