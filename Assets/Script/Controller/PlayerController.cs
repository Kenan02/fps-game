using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -10f;
        public float jumpHeight = 4f;
        private float moveCounter;
        private float idleCounter;
        public float groundDistance = 0.4f;
        Vector3 moveDir = Vector3.zero;
        public LayerMask groundMask;
        public Transform groundCheck;
        public Transform weaponParent;
        private Vector3 weaponParentOrigin;
        private Vector3 targetWeaponBobPos;
        Vector3 velocity;
        bool isGrounded;

        #endregion


        #region Monobehaviour

        private void Start()
        {
            weaponParentOrigin = weaponParent.localPosition;
            controller = GetComponent<CharacterController>();
        }
        void Update()
        {
            Movement_ctrl();

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }


        }

    
        #endregion


        #region Private Methods
        void Movement_ctrl()
        {


            float xMov = Input.GetAxis("Horizontal");
            float zMov = Input.GetAxis("Vertical");

            //HeadBob
            if (xMov == 0 && zMov == 0)
            {
                HeadBob(idleCounter, 0.015f, 0.015f);
                idleCounter += Time.deltaTime;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPos, Time.deltaTime * 3f);
            }
            else
            {
                HeadBob(moveCounter, 0.035f, 0.035f);
                moveCounter += Time.deltaTime * 3f;
                weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPos, Time.deltaTime * 6f);

            }
                Vector3 move = transform.right * xMov + transform.forward * zMov;
                controller.Move(move * speed * Time.deltaTime);
                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);

        }

        void HeadBob(float p_z, float p_x_instensity, float p_y_intensity)
        {
            targetWeaponBobPos = weaponParentOrigin + new Vector3(Mathf.Cos(p_z) * p_x_instensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
        }

        #endregion
    }
