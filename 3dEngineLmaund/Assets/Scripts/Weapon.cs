using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //parameters
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 33f;
    [SerializeField] float rateOfFire = 1f;

    //cached references
    [SerializeField] Camera playerCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Ammo ammo;
    [SerializeField] AmmoType ammoType;

    //states
    [SerializeField] bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
           
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        if (ammo.GetAmmoAmount(ammoType) > 0 )
        {
            ProcessRaycast();
            PlayMuzzleFlash();
            ammo.ReduceAmmo(ammoType);
        }
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
           
    }

    private void ProcessRaycast()
    {
        RaycastHit thingWeHit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out thingWeHit, range))
        {
            print(thingWeHit.transform.name);
            CreateHitVFX(thingWeHit);
            if (thingWeHit.transform.gameObject.layer == 10)
            {
                EnemyHealth target = thingWeHit.transform.GetComponent<EnemyHealth>();
                if(target == null) { return; } 
                target.TakeDamage(damage);
            }
            else
            {
                return;
            }

        }
    }   
        

    private void CreateHitVFX(RaycastHit thingWeHit)
    {
        GameObject impact = Instantiate(hitVFX, thingWeHit.point, Quaternion.identity);
        Destroy(impact, 0.1f);
    }
}
