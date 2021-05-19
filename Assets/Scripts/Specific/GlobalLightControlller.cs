using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightControlller : MonoBehaviour 
{
    public Light2D light;
    public float timeDayComing;

    public float DefaultNightTime;
    public float DefaultDayTime;
    private float NightTime;
    private float DayTime;
    private float cof = 0.01f; //Коеффициент

    private bool isDay = true;

    private void Start() 
    {
        DayTime = DefaultDayTime;
        NightTime = DefaultNightTime;

        StartCoroutine(LightController());
    }
    IEnumerator LightController() 
    {
        yield return new WaitForSeconds(timeDayComing);

        if (light.intensity > 0.15f && isDay == true) //Уменьшается свет, наступает ночь
        {
            light.intensity -= cof;
            DayTime = DefaultDayTime;
        }    
            

        if (light.intensity <= 0.15f && NightTime != 0) NightTime--; //Ночь наступила, ждет пока пройдет


        if (light.intensity <= 0.15f && NightTime == 0) isDay = false; //Ночь прошла, переключает режим на переход в день


        if (light.intensity < 0.9f && isDay == false) // Увеличивается свет, наступает день
        {
            light.intensity += cof;
            NightTime = DefaultNightTime;
        }

        if (light.intensity <= 0.15f && DayTime != 0) DayTime--; //Наступил день, ждет пока пройдет


        if (light.intensity >= 0.9f && isDay == false && DayTime == 0) isDay = true; //День прошел, переключает режим на переход в ночь

        StartCoroutine(LightController());
    }
}