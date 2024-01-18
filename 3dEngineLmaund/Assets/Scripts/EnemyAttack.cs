using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //parameters
    [SerializeField] float damage = 20f;

    //cached ref
    [SerializeField] PlayerHealth Target;

    public void AttackHitEvent()
    {
        
        Target.TakeDamage(damage);
    }


}
