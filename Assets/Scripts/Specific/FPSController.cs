using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int fps;

    private void Awake() => Application.targetFrameRate = fps;
}
