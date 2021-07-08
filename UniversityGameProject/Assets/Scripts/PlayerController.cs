using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Eevee,
    Flareon
}

public class PlayerController : MonoBehaviour
{
    public PlayerType PlayerType = PlayerType.Eevee;
    public float Speed = 2f;
    public float InterpFactor = .03f;
    public Camera Camera;
    public GameObject CameraContainer;
    public float Distance = 10f;

    public ParticleController UndergroundParticles;
    public ParticleController[] PuffParticles;

    private bool ButtonDown = false;
    private float MScroll = 0;

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
    bool underFloor = false;
    bool changing = false;
    float changeTime = 0;
    Vector3 tempPosition;
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

        ScrollSize += MScroll * 2f;

        if (Input.GetKeyDown(KeyCode.E) && underFloor && !changing)
        {
            underFloor = false;
            changing = true;
            tempPosition = transform.position + new Vector3(0, .5f, 0);
            UndergroundParticles.Pause();
        }
        if (Input.GetKeyDown(KeyCode.E) && !underFloor && !changing)
		{
            underFloor = true;
            changing = true;
            tempPosition = transform.position + new Vector3(0, -.5f, 0);
            Puff();
        }
        
        if (changing)
		{
            changeTime += .1f;
			if (changeTime >= 10f)
			{
				if (underFloor)
				{
                    UndergroundParticles.Play();
                }
                changing = false;
                changeTime = 0;
			}
            transform.position = Vector3.Lerp(
                transform.position,
                tempPosition,
                .01f
            );
        }

        
    }
    public void Puff()
	{
		foreach (var item in PuffParticles)
		{
            item.Play();
		}
	}
}
