using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmountInThisPickup = 10;
    
    [SerializeField] AmmoType ammoType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseAmmo(ammoType, ammoAmountInThisPickup);
            Destroy(gameObject);
        }
    }

}
