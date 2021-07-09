using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeParticle : MonoBehaviour
{
    internal ParticleController Controller;
    Quaternion Direction;
    internal float speed = 1f;

    Vector3 lscale;
    bool start = false;
    int time = 0;
    
    void FixedUpdate()
    {
		if (Controller != null && !start)
		{
            start = true;
            var a = Random.Range(-Controller.AdditionalAngle, Controller.AdditionalAngle);
            var b = Random.Range(-Controller.AdditionalAngle, Controller.AdditionalAngle);

            Direction = Quaternion.AngleAxis(Controller.AngleX + a + (Controller.PositionType == PositionType.Relative ? Controller.transform.rotation.eulerAngles.y : 0), Vector3.up) *
                Quaternion.AngleAxis(Controller.AngleY + b + (Controller.PositionType == PositionType.Relative ? -Controller.transform.rotation.eulerAngles.x : 0), Vector3.left);
            transform.rotation = Direction;
            transform.position = Controller.transform.position;
            transform.localScale = transform.localScale * (Random.Range(0, Controller.SizeOffset) + 1);
            lscale = transform.localScale;
			if (Controller.HideType == HideType.EaseIn || Controller.HideType == HideType.EaseInOut)
			{
                transform.localScale = Vector3.zero;
			}

        }
        
		if (start)
		{
            
            transform.Translate(Vector3.forward * speed * .5f * Time.deltaTime);
            time++;
            if (Controller.DestroyType == DestroyType.Distance && Vector3.Distance(Controller.transform.position, transform.position) > Controller.Distance ||
                Controller.DestroyType == DestroyType.Time && Controller.DestroyTime < time)
			{
				switch (Controller.HideType)
				{
                    case HideType.Destroy: Destroy(gameObject); break;
                    case HideType.EaseOut:
                        transform.localScale -= Vector3.one * Controller.InterpFactorOut;
                        if (transform.localScale.x <= 0)
                        {
                            Destroy(gameObject);
                        }
                        break;
                    case HideType.EaseInOut:
                        transform.localScale -= Vector3.one * Controller.InterpFactorOut;
                        if (transform.localScale.x <= 0)
                        {
                            Destroy(gameObject);
                        }
                        break;
                }
			}
			else
			{
                if (Controller.HideType == HideType.EaseIn || Controller.HideType == HideType.EaseInOut)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, lscale, Controller.InterpFactorIn);
                }
            }
		}
    }
}
