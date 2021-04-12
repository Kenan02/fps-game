using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kenan.daniel.spel;


public class PickUpController : MonoBehaviour
{
    public Weapon gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;
    public float PickUpRange;
    public float dropForwardForce, dropUpwardForce;
    public bool equipped;
    public static bool slotFull;


    private void Start()
    {
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;

        }
    }


    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= PickUpRange && Input.GetKeyDown(KeyCode.F) && !slotFull) PickUp();


        if(equipped && Input.GetKeyDown(KeyCode.G)) Drop();
    }

    private void PickUp()
    {

        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        gunScript.enabled = true;

    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);
        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        rb.isKinematic = false;
        coll.isTrigger = false;

        gunScript.enabled = false;

    }

}