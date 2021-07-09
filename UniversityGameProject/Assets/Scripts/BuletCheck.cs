using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletCheck : MonoBehaviour
{
    private bool PlayerInZone = false;

    public GameObject Player;

    private int counter = 0;

    void Update()
    {
        if (PlayerInZone)
        {

            var light = GameObject.FindGameObjectWithTag("TagForPokebols");

            light.GetComponent<Light>().intensity = 10;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().mass = 100f;
            GetComponent<Rigidbody>().drag = 100f;
            GetComponent<Rigidbody>().angularDrag = 100f;

            transform.position += new Vector3(0, .6f * Time.deltaTime, 0);
            counter++;

            if(counter >= 300)
            {
                Destroy(GameObject.FindGameObjectWithTag("Bulet"));
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInZone = true;
        }
    }
}
