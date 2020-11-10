using UnityEngine;


//https://www.youtube.com/watch?v=wdL4_60ya90
namespace kenan.daniel.spel
{
    public class Weapon : MonoBehaviour
    {
        #region Variables
        public Gun[] loadout;
        public Transform weaponParent;
        private int currentIndex;

        //bullet holes
        public GameObject bulletHolePrefab;
        public LayerMask canBeShot;
        private GameObject currentWeapon;

        //recoil
       

        #endregion

        #region MonoBehaviour Callbacks

       
        void Update()
        {

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
        #endregion


        #region Private Methods
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
            Transform t_spawn = transform.Find("Cameras/Camera");

            //bloom
            Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;
            t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.up;
            t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.right;
            t_bloom -= t_spawn.position;
            t_bloom.Normalize();

            //raycast
            RaycastHit t_hit = new RaycastHit();
            if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, canBeShot))
            {
                GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                Destroy(t_newHole, 5f);
            }

            // gun fx


        }

        #endregion

    }
}
