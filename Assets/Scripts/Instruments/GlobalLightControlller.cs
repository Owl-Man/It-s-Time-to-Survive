using UnityEngine;
using System.Collections;
using Player;

public class GlobalLightControlller : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light;
    [SerializeField] private Values values;

    [SerializeField] private GameObject[] AdditionalLights;

    [SerializeField] private float timeDayComing;
    [SerializeField] private float DefaultNightTime, NightTime;
    [SerializeField] private float DefaultDayTime, DayTime;

    private const float Cof = 0.01f; //Коеффициент

    private bool _isDay = true;

    private void Start()
    {
        DayTime = DefaultDayTime;
        NightTime = DefaultNightTime;
        
        AdditionalLightsUpdate();

        StartCoroutine(LightController());
    }

    private IEnumerator LightController()
    {
        yield return new WaitForSeconds(timeDayComing);

        if (light.intensity > 0.15f && _isDay) //Уменьшается свет, наступает ночь
        {
            light.intensity -= Cof;
            DayTime = DefaultDayTime;
        }


        if (light.intensity <= 0.15f && NightTime != 0) NightTime--; //Ночь наступила, ждет пока пройдет


        if (light.intensity <= 0.15f && NightTime == 0) //Ночь прошла, переключает режим на переход в день
        {
            _isDay = false;
            values.ChangeDaysValue(1);
        } 

        if (light.intensity < 0.9f && !_isDay) // Увеличивается свет, наступает день
        {
            light.intensity += Cof;
            NightTime = DefaultNightTime;
        }

        AdditionalLightsUpdate();

        if (light.intensity < 0.9f && DayTime != 0) //Наступил день, ждет пока пройдет
        {
            DayTime--;
        }
        
        //День прошел, переключает режим на переход в ночь
        if (light.intensity >= 0.9f && !_isDay && DayTime == 0) _isDay = true;
        
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