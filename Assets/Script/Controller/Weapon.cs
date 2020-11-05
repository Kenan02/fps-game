using UnityEngine;
//https://www.youtube.com/watch?v=wdL4_60ya90
namespace kenan.daniel.spel
{
    public class Weapon : MonoBehaviour
    {
        #region Variables
        public Gun[] loadout;
        public Transform weaponParent;

        public GameObject bulletholePrefab;
        public LayerMask canbeshot;
        private GameObject currentWeapon;
        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);
            

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

            GameObject t_newEquipment = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            t_newEquipment.transform.localEulerAngles = Vector3.zero;

            currentWeapon = t_newEquipment;
        }

        void Shoot()
        {
            Transform t_spawn = transform.Find("Cameras/normal Cam");

            RaycastHit t_hit = new RaycastHit();
            if(Physics.Raycast(t_spawn.position, t_spawn.forward, out t_hit, 1000f, canbeshot))
            {
                GameObject t_newHole = Instantiate(bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                Destroy(t_newHole, 5f);
                
            }
        }
        #endregion

    }
}
