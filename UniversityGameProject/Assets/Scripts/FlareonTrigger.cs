using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareonTrigger : MonoBehaviour
{
    public PlayerController Player;
	public Canvas UI;
    GameObject interract;

    bool spawn = false;
	bool changing = false;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == Player.gameObject && Player.PlayerType == PlayerType.Flareon)
		{
			if (!spawn)
			{
				interract = UI.GetComponent<UIController>().CreateInterractMark();
				interract.transform.SetParent(UI.transform);
				spawn = true;
			}
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		spawn = false;
		Destroy(interract);
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (interract != null && Player.PlayerType == PlayerType.Flareon)
		{
			interract.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, .3f, 0));
		}

    }
}
