using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Eevee,
    Flareon,
    Vaporeon,
    Leafeon
}

public class PlayerController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Stamina = 1f;
    public GameObject StaminaBar;
    public PlayerType PlayerType = PlayerType.Eevee;
    public float Speed = 2f;
    public float InterpFactor = .03f;
    public Camera Camera;
    public GameObject VaporeonWater;

    public ParticleController WaterSpalsh;
    public ParticleController UndergroundParticles;
    public ParticleController[] PuffParticles;

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

    public GameObject[] PlayerIcons;

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
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            SwitchType(0);
		}
        if (Input.GetKeyDown(KeyCode.Alpha2))
		{
            SwitchType(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
		{
            SwitchType(2);
        }
	}
    void SwitchType(int i)
	{
		foreach (var item in PlayerIcons)
		{
            item.SetActive(false);
		}
        PlayerIcons[i].SetActive(true);
    }
    bool underFloor = false;
    bool changing = false;
    float changeTime = 0;
    bool ability = false;
    bool staminaEnded = false;
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

        if (Mathf.Sign(MScroll) == -1 && ScrollSize >= MinSize)
        {
            ScrollSize += MScroll * 2f;
        }
        if (Mathf.Sign(MScroll) == 1 && ScrollSize <= MaxSize)
            ScrollSize += MScroll * 2f;

		if (Stamina > 0)
            staminaEnded = false;
		if (Stamina <= 0)
		{
            staminaEnded = true;
            Stamina = 0;
		}
		if (PlayerType == PlayerType.Leafeon)
		{
            if (Input.GetKeyDown(KeyCode.E) && underFloor && !changing || staminaEnded && underFloor)
            {
                underFloor = false;
                changing = true;
                ability = false;
                staminaEnded = false;
                tempPosition = transform.position + new Vector3(0, .5f, 0);
                UndergroundParticles.Pause();
            }
            if (Input.GetKeyDown(KeyCode.E) && !underFloor && !changing && Stamina > 0)
            {
                ability = true;
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

		if (ability)
		{
            Stamina -= .25f * Time.deltaTime;
        }
        StaminaBar.transform.localScale = new Vector3(
                Stamina,
                StaminaBar.transform.localScale.y,
                StaminaBar.transform.localScale.z
                );
    }
    public void Puff()
	{
		foreach (var item in PuffParticles)
		{
            item.Play();
		}
	}
}
