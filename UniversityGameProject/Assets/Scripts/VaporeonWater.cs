using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporeonWater : MonoBehaviour
{
    internal bool InWater = false;
	public PlayerController Player;
	bool changing = true;
	Vector3 tempPosition;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.gameObject == Player.gameObject)
		{
			tempPosition = Player.transform.position + new Vector3(0, -.1f, 0);
			InWater = true;
			changing = false;
			Player.WaterSpalsh.Play();
			Player.Puff();
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.gameObject == Player.gameObject)
		{
			tempPosition = Player.transform.position + new Vector3(0, .1f, 0);
			InWater = false;
			changing = false;
			Player.WaterSpalsh.Pause();
			Player.Puff();
		}
	}
	
    void FixedUpdate()
    {
		if (!changing)
		{
			changing = true;
			Player.transform.position = tempPosition;
		}
    }
}
