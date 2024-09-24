using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordController : MonoBehaviour
{
    [Header("Settings")]
    public static bool hasDealtDamage;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] float weaponLength;
    [SerializeField] int weaponDamage;
    [SerializeField] private GameObject damageParticle;
    // Start is called before the first frame update
    void Start()
    {
        EnemyController.canDealDamage = false;
        hasDealtDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyController.canDealDamage&& !hasDealtDamage)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, -transform.up * weaponLength, Color.red);
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, playerMask))
            {
                if (hit.transform.TryGetComponent(out PlayerHealth health))
                {
                    if (PlayerAnimation.canBlock)
                    {
                        Debug.Log("block");
                        health.Block();
                        CinemachineShake.Instance.ShakeCamera(4f, 0.5f);
                    }
                    else
                    {

                    Debug.Log("player hasar yedi");
                    health.TakeDamage(weaponDamage);
                    hasDealtDamage=true;

                        DamageParticle(hit.point);
                        CinemachineShake.Instance.ShakeCamera(4f, 0.5f);


                    }
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
        Debug.DrawRay(transform.position, -transform.up * weaponLength, Color.red);

    }
}
