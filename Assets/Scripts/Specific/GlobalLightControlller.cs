using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;
using System.Collections;

public class GlobalLightControlller : MonoBehaviour
{
    public Light2D light;
    public Values values;
    public float timeDayComing;

    public GameObject[] AdditionalLights;

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
        
        AdditionalLightsUpdate();

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


        if (light.intensity <= 0.15f && NightTime == 0) isDay = false; //Ночь прошла, переключает режим на переход в ден

        if (light.intensity < 0.9f && isDay == false) // Увеличивается свет, наступает день
        {
            light.intensity += cof;
            NightTime = DefaultNightTime;
        }

        AdditionalLightsUpdate();

        if (light.intensity <= 0.15f && DayTime != 0) //Наступил день, ждет пока пройдет
        {
            DayTime--;
            values.ChangeDaysValue(1);
        }

        if (light.intensity >= 0.9f && isDay == false && DayTime == 0) isDay = true; //День прошел, переключает режим на переход в ночь

        StartCoroutine(LightController());
    }

    private void AdditionalLightsUpdate()
    {
        if (light.intensity <= 0.7) SetAllAdditionalLightsState(true);

        if (light.intensity > 0.7) SetAllAdditionalLightsState(false);
    }

    private void SetAllAdditionalLightsState(bool state)
    {
        for (int i = 0; i < AdditionalLights.Length; i++)
        {
            AdditionalLights[i].SetActive(state);
        }
    }
}