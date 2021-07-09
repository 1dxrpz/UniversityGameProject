using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class WireArcExample : MonoBehaviour
{
    public float shieldArea;
}
[CustomEditor(typeof(WireArcExample))]
public enum HideType
{
    Destroy,
    EaseInOut,
    EaseOut,
    EaseIn,
}
public enum PositionType
{
    Relative,
    Absolute
}
public enum DestroyType
{
    Distance,
    Time
}

public class ParticleController : MonoBehaviour
{
    public HideType HideType = HideType.Destroy;
    public PositionType PositionType = PositionType.Relative;
    public DestroyType DestroyType = DestroyType.Distance;
    public bool IsPaused = true;
    public bool EnableTimer = false;

    [Min(0)]
    public float Timer = 1f;

    [Range(0, 360)]
    public float AngleX = 0f;
    [Range(0, 360)]
    public float AngleY = 0f;
    [Range(1, 40)]
    public float Radius = 10f;
    [Range(0, 10)]
    public float Distance = 1f;
    public VolumeParticle ParticleMesh;

    [Range(0, 1)]
    public float PositionOffset = 0;

    [Min(0)]
    public float Speed = 50f;
    [Min(0)]
    public float SpeedOffset = 0;

    [Min(0)]
    public float Count = 30;
    [Min(0)]
    public float CountOffset = 0;
    internal float AdditionalAngle = 0;

    [Min(0)]
    public float SizeOffset = 0;
    [Min(0)]
    public int DestroyTime = 1000;

    [Range(0, .5f)]
    public float InterpFactorIn = .01f;
    [Range(0, .5f)]
    public float InterpFactorOut = .01f;
    
    void OnDrawGizmosSelected()
    {
        Quaternion a = Quaternion.AngleAxis(AngleX + (PositionType == PositionType.Relative ? transform.rotation.eulerAngles.y : 0), Vector3.up);
        Quaternion b = Quaternion.AngleAxis(AngleY + (PositionType == PositionType.Relative ? -transform.rotation.eulerAngles.x : 0), Vector3.left);

        Gizmos.DrawRay(transform.position, a * b * Vector3.forward * Distance);
        Handles.DrawWireArc(transform.position + a * b * Vector3.forward * Distance,
            a * b * Vector3.forward * Distance, a * b * Vector3.up, 360, Radius / 20);
    }

    float counter = 0;
    float timerCounter = 0;
    void FixedUpdate()
    {
		if (EnableTimer && !IsPaused)
		{
            timerCounter += .1f;
			if (timerCounter >= Timer)
			{
                IsPaused = true;
                timerCounter = 0;
			}
		}
        AdditionalAngle = Mathf.Atan(Radius / (Distance * 20)) * Mathf.Rad2Deg * Random.Range(0, PositionOffset);
		if (!IsPaused)
		{
            counter++;
        }
		if (counter > Count + Random.Range(0, CountOffset))
		{
            counter = 0;
            var temp = Instantiate(ParticleMesh);
            temp.Controller = this;
            temp.speed = Speed * .05f * (Random.Range(0, SpeedOffset * .05f) + 1);
        }
    }
    public void Pause()
	{
        IsPaused = true;
	}
	public void Play()
	{
        IsPaused = false;
    }
    public void Toggle()
	{
        IsPaused = !IsPaused;
	}
}
