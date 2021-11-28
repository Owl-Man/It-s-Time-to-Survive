using Player;
using UnityEngine;

public class UseFood : MonoBehaviour
{
	public Food food;
	private Indicators _indicators;

	private void Start() => _indicators = LinkManager.instance.indicators;

	public void EatFood() 
	{
		_indicators.health += food.heal;
		_indicators.satiety += food.satiety;
		_indicators.UpdateAllValues();

		Debug.Log("Eaten, +" + food.heal + " hearth, +" + food.satiety + " satiety");
	}
}
