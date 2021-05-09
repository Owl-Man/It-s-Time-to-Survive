using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    [SerializeField] private string MainPlayScene;
    public void OnPlayButtonClick() => SceneManager.LoadScene(MainPlayScene);
    public void OnExitButtonClick() => Application.Quit();
}