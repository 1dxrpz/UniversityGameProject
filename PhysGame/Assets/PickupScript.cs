using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float Range;
    public float MoveForce = 250;
    public Transform HoldParent;
    public GameObject Object;
    public PlayerInventory Inv;

    void Start()
    {
        
    }

    void Update()
    {
        if (!Inv.Inventory.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Object == null)
                {
                    RaycastHit Hit;
                    if (Physics.Raycast(
                        transform.position,
                        transform.TransformDirection(Vector3.forward),
                        out Hit,
                        Range))
                    {
                        Pickup(Hit.transform.gameObject);
                    }
                }
                else
                {
                    Drop();
                }
            }
        }
        if (Object != null)
        {
            Move();
        }
    }
    void Move()
	{
        //Object.transform.position = HoldParent.transform.position;

        Vector3 MoveDir = HoldParent.position - Object.transform.position;
        Object.GetComponent<Rigidbody>().AddForce(MoveDir * MoveForce);

        if (Vector3.Distance(Object.transform.position, HoldParent.transform.position) < .2f)
		{
            //Vector3 MoveDir = HoldParent.position - Object.transform.position;
            //Object.GetComponent<Rigidbody>().AddForce(MoveDir * MoveForce);
		}
	}
    void Pickup(GameObject obj)
	{
		if (obj.GetComponent<Rigidbody>())
		{
            Rigidbody Rig = obj.GetComponent<Rigidbody>();
            Rig.useGravity = true;
            Rig.drag = 10;
           // Rig.transform.parent = HoldParent;
            Object = obj;
        }
	}
    void Drop()
	{
        Rigidbody Rig = Object.GetComponent<Rigidbody>();
        Rig.useGravity = true;
        Rig.drag = 1;

       // Object.transform.parent = null;
        Object = null;
	}
}
