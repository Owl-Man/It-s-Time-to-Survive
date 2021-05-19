using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int fps;

    private void Start() => Application.targetFrameRate = fps;
}
