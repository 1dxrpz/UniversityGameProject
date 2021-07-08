using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 2f;
    public float InterpFactor = .03f;
    public Camera Camera;
<<<<<<< HEAD
<<<<<<< HEAD


    public ParticleController UndergroundParticles;
    public ParticleController[] PuffParticles;

=======
    public GameObject CameraContainer;
    public float Distance = 10f;
    
>>>>>>> parent of e900370 (particles & first ability)
=======
    public GameObject CameraContainer;
    public float Distance = 10f;
    
>>>>>>> parent of e900370 (particles & first ability)
    private bool ButtonDown = false;
    private float MScroll = 0;
    public float MaxSize = 4f;
    public float MinSize = 1f;

    float ScrollSize;

    Vector3 Velocity = Vector3.zero;
    Vector3 Direction = Vector3.zero;

    Vector3 Up = new Vector3(1, 0, 1);
    Vector3 Left = new Vector3(-1, 0, 1);

    Rigidbody Rigidbody;

    void Start()
    {
        ScrollSize = Camera.orthographicSize;
        Rigidbody = GetComponent<Rigidbody>();
    }

    private float rt = 0;
    private float angle = 0;

    float prevangle = 0;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		
	}
	private void FixedUpdate()
	{
        prevangle = angle;
        angle = Mathf.Atan2(Vector3.Normalize(Direction).z, -Vector3.Normalize(Direction).x) * Mathf.Rad2Deg + 90;

        Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, ScrollSize, InterpFactor);

        Velocity = Vector3.Lerp(Velocity, ButtonDown ? Vector3.Normalize(Direction) * Time.deltaTime * Speed : Vector3.zero, InterpFactor);
        Rigidbody.velocity = Velocity;


        Camera.transform.parent.position = Vector3.Lerp(Camera.transform.parent.position, transform.position, InterpFactor);

		if (Mathf.Abs(angle - rt) > 180)
		{
			if (angle > rt)
			{
                rt = Mathf.Lerp(rt + 360, angle, InterpFactor);
            }
			else
			{
                rt = Mathf.Lerp(rt - 360, angle, InterpFactor);
            }
        }
		else
		{
            rt = Mathf.Lerp(rt, angle, InterpFactor);
        }

        
        transform.rotation = Quaternion.Euler(0, rt , 0);
    }
	void Update()
    {
        ButtonDown = Input.anyKey;
        
        Direction = Vector3.Normalize(Direction);
        
        if (Input.GetKey(KeyCode.W))
            Direction += Up;
        if (Input.GetKey(KeyCode.A))
            Direction += Left;
        if (Input.GetKey(KeyCode.D))
            Direction += -Left;
        if (Input.GetKey(KeyCode.S))
            Direction += -Up;

        MScroll = Input.GetAxis("Mouse ScrollWheel");


<<<<<<< HEAD
        if(Mathf.Sign(MScroll) == -1 && ScrollSize >= MinSize)
        {
            ScrollSize += MScroll * 2f;
        }
        if(Mathf.Sign(MScroll) == 1 && ScrollSize <= MaxSize)
            ScrollSize += MScroll * 2f;


		if (MScroll > 0)
		{

		}

=======
		if (MScroll > 0)
		{

		}

        
>>>>>>> parent of e900370 (particles & first ability)
    }
}
