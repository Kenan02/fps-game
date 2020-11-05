using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kenan.daniel.spel{
public class Sway : MonoBehaviour
{
    #region Variables
    public float intesity;
    public float smooth;
    private Quaternion origin_rotation;
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        origin_rotation = transform.localRotation;
    }

    private void Update()
    {
        UpdateSway();
    }
     
    #endregion

    #region Private Methods
    private void UpdateSway()
    {
        //controlls
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y");

        //calculate target rotation
        Quaternion t_x_adj = Quaternion.AngleAxis(-intesity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intesity * t_x_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * t_x_adj * t_y_adj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation , target_rotation, Time.deltaTime * smooth);

    }
    #endregion
}
}
