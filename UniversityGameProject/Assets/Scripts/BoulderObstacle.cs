using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderObstacle : MonoBehaviour
{
    Rigidbody Rigidbody;
    public ParticleController[] particleControllers;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    bool Fall = false;
    void Update()
    {
		if (Rigidbody.transform.position.y > .8)
		{
            Fall = false;
		}
		if (Rigidbody.transform.position.y < .8 && !Fall)
		{
            Rigidbody.isKinematic = true;
            Fall = true;
			foreach (var item in particleControllers)
			{
                item.Play();
			}
		}
    }
}
