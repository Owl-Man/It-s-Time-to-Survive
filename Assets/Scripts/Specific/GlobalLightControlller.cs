using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightControlller : MonoBehaviour 
{
    public Light2D light;

    public float timeDayComing;
    private float cof = 0.01f; //Коеффициент

    private bool isDay = true;

    private void Start() 
    {
        StartCoroutine(LightController());
    }

    IEnumerator LightController() 
    {
        yield return new WaitForSeconds(timeDayComing);

        if (light.intensity > 0.2f && isDay == true) 
        {
            light.intensity -= cof;
        }

        if (light.intensity <= 0.2f)
        {
            isDay = false;
        }

        if (light.intensity < 0.9f && isDay == false) 
        {
            light.intensity += cof;
        }

        if (light.intensity >= 0.9f && isDay == false) 
        {
            isDay = true;
        }

        StartCoroutine(LightController());
    }
}