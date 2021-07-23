using UnityEngine;

public class UseFood : MonoBehaviour
{
	public Food food;
	private Indicators indicators;

	private void Start() => indicators = LinkManager.instance.indicators;

	public void EatFood() 
	{
		indicators.health += food.heal;
		indicators.satiety += food.satiety;
		indicators.UpdateAllValues();

		Debug.Log("Eaten, +" + food.heal + " hearth, +" + food.satiety + " satiety");
	}
}
