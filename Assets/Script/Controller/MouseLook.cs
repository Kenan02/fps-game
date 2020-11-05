using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Variables

    public float Sens = 100f;
    float xRot;
    public Transform playerBody;
    public Transform weapon;
    #endregion 

    #region Monobehaviour
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sens * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        weapon.localRotation = transform.localRotation;
        playerBody.Rotate(Vector3.up * mouseX);
    }
    #endregion
    
    #region Private Methods
    #endregion
}
