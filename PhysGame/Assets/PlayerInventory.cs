using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public FirstPersonController fpc;
    public Camera Inventory;
    public GameObject Spawner;
    Stack<GameObject> objectSpawnStack = new Stack<GameObject>();
    void Start()
    {
        Inventory.enabled = false;
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
            fpc.cameraCanMove = !fpc.cameraCanMove;
            //fpc.playerCanMove = !fpc.playerCanMove;
            Inventory.enabled = !Inventory.enabled;
            fpc.lockCursor = !Inventory.enabled;
            Cursor.lockState = fpc.lockCursor ? CursorLockMode.Locked : CursorLockMode.Confined;
            Cursor.visible = Inventory.enabled;
        }
		if (Inventory.enabled)
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
                //print(Input.mousePosition);
                RaycastHit Hit;
                try
                {
                    if (Physics.Raycast(
                        Inventory.ScreenPointToRay(Input.mousePosition),
                        out Hit))
                    {
                        if (Hit.transform.gameObject.layer == 6)
                        {
                            var obj = Instantiate(Hit.transform.gameObject);
                            obj.transform.position = Spawner.transform.position;
                            Rigidbody rb = obj.GetComponent<Rigidbody>();
                            rb.isKinematic = false;
                            rb.useGravity = true;
                            obj.transform.rotation = Quaternion.Euler(Vector3.zero);
                            obj.transform.localScale = Vector3.one;
                            obj.layer = 0;
                            objectSpawnStack.Push(obj);
                        }
                    }
                }
				catch
				{

				}
            }
		}
        if (Input.GetKeyDown(KeyCode.Z) && objectSpawnStack.Count > 0)
		{
            Destroy(objectSpawnStack.Peek());
            objectSpawnStack.Pop();
            print("deleted");
		}
    }
}
