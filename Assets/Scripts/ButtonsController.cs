using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
	[SerializeField] private string MainPlayScene;

	[SerializeField] private GameObject InventoryPanel;
	[SerializeField] private GameObject DescriptionItemPanel;

    public void OnInventoryButtonClick() => InventoryPanel.SetActive(true);

    public void OnBackForInventoryButtonClick() => InventoryPanel.SetActive(false);

    public void IsOnItemButtonClick(bool state) => DescriptionItemPanel.SetActive(state);

    public void OnRestartButtonClick() => SceneManager.LoadScene(MainPlayScene);

    public void OnMenuButtonClick() => SceneManager.LoadScene("Menu");
}
