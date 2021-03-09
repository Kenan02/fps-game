using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kenan.daniel.spel;

    public class Weapon : MonoBehaviour
    {
        public Gun[] loadout;
        public Transform weaponParent;
        private int currentIndex;
        public ParticleSystem muzzleFlash;

        //bullet holes
        public GameObject bulletHolePrefab;
        public LayerMask canBeShot;
        private GameObject currentWeapon;

        //Ammo/shooting
        public float dmg = 10f;
        public float range = 100f;
        public int maxAmmo = 10;
        public Camera fpsCam;
        private int currentAmmo = -1;
        public float reloadTime = 1f;
       
        void Update()
        {
            if(currentAmmo <= 0)
            {
                Reload();
                return;
            }

            //Equip Weapons
            if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) Equip(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) Equip(3);

            if (currentWeapon != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }

            }
        }

        void Reload()
        {
            Debug.Log("Realoding...");
            currentAmmo = maxAmmo;
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
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log("shooting");
            }
            

     }
     

    }
