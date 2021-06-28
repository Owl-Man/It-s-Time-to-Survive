using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] private GameObject Effects;

    private void Start() => UpdateEffects();

    public void UpdateEffects() 
    {
        if (PlayerPrefs.GetInt("isEffectsEnabled") == 1) Effects.SetActive(true);
        else Effects.SetActive(false);
    }
}