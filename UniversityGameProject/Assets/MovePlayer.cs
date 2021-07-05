using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float shiftSpeed = 1.0f;

    void Update()
    {
        //Vector3 MoveFB = new Vector3();
        if (Input.GetKey(KeyCode.S))
        {
            //MoveFB += Vector3.forward;
            transform.Translate(Vector3.forward * Time.deltaTime * shiftSpeed);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //MoveFB += Vector3.back;
            transform.Translate(Vector3.back * Time.deltaTime * shiftSpeed);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftSpeed = 3.0f;
            GetComponent<Animator>().speed = 4;

        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            shiftSpeed = 1.0f;
            GetComponent<Animator>().speed = 2;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().speed = 0;
        }
        
        //MoveFB = MoveFB * Time.deltaTime;
        //transform.position += MoveFB;
        //transform.Translate(MoveFB);

        Vector3 ChangePos = new Vector3();
        if (Input.GetKey(KeyCode.A))
        {
            ChangePos += new Vector3(0, -2, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ChangePos += new Vector3(0, 2, 0);
        }

        ChangePos = ChangePos * Time.deltaTime * 100;
        transform.Rotate(ChangePos);

    }
}
