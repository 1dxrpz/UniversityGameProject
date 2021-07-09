using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnter : MonoBehaviour
{
    //public GameObject Player;
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerController>())
		{
            PlayerController player = other.GetComponent<PlayerController>();
            transform.parent.GetComponent<BoxCollider>().enabled = !(player.PlayerType == PlayerType.Vaporeon);
        }
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
