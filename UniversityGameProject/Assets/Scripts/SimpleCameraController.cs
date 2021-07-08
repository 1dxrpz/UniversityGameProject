using UnityEngine;

namespace UnityTemplateProjects
{
    public class SimpleCameraController : MonoBehaviour
    {
        float a = 27;
        float temp = 0;
        private void Update()
        {
            temp = GetComponent<Camera>().fieldOfView;
            float mw = Input.GetAxis("Mouse ScrollWheel");
            
            a += temp < 30 && Mathf.Sign(mw) == -1 ||
                temp > 10 && Mathf.Sign(mw) == 1 ? -mw * 10f : 0;

            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, a, .1f);
        }
        
    }
}