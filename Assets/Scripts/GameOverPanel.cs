using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour 
{
	[SerializeField] private Text KillsText;
    [SerializeField] private Text DaysText;
    [SerializeField] private Text LevelText;

	public Values values;

	private void Awake() => SyncPlayerValues();

	private void SyncPlayerValues() 
	{
		KillsText.text = values.Kills.ToString();
		DaysText.text = values.Days.ToString();
		LevelText.text = values.Level.ToString();
	}
}