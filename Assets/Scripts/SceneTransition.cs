using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour 
{
    [SerializeField] private string scene;

    [SerializeField] private GameObject TransitionButton;
    
    private void OnTriggerEnter2D(Collider2D other) => TransitionButton.SetActive(true);
    
    private void OnTriggerExit2D(Collider2D other) => TransitionButton.SetActive(false);

    public void OnTransitionButtonClick() => SceneManager.LoadScene(scene);
}