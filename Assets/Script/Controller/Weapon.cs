using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kenan.daniel.spel;

public class Weapon : MonoBehaviour
{
    public Gun[] loadout;
    public Transform weaponParent;
    private int currentIndex;


    //bullet holes
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;
    private GameObject currentWeapon;


    //Ammo/shooting
    public float Damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 30f;

    private float nextTimeToFire = 0f;

    void Update()
    {

        //Equip Weapons
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);

        if (currentWeapon != null)
        {
       
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) 
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

    }


    void Equip(int p_ind)
    {
        if (currentWeapon != null) Destroy(currentWeapon);

        currentIndex = p_ind;

        GameObject t_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;
        t_newEquipment.transform.localPosition = Vector3.zero;

        currentWeapon = t_newEquipment;
    }

    void Shoot()
    {

        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Shooting");

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(Damage);
            }

            if(hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);

        }


    }
}
    
