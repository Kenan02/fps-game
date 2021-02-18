
using UnityEngine;
using kenan.daniel.spel;
public class MouseLook : MonoBehaviour
{
    #region Variables

    public float Sens = 100f;
    float xRot;
    public Transform playerBody;
    public Transform weapon;
    public Transform cams;
    #endregion 

    #region Monobehaviour
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Mouse Rotation
        float mouseX = Input.GetAxis("Mouse X") * Sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //Weapon rotation
        weapon.localRotation = transform.localRotation;
        weapon.rotation = cams.rotation;
    }
    #endregion
    
   
}
