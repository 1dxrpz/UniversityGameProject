using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletCheck : MonoBehaviour
{
    private bool PlayerInZone = false;

    public GameObject Player;
    //public GameObject Pokebol;

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
