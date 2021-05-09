using UnityEngine;

public class UseFood : LinkManager
{
	public Food food;
	private Indicators indicators;

	private void Start() => indicators = ManagerIndicators;

	public void EatFood() 
	{
		indicators.health += food.heal;
		indicators.satiety += food.satiety;

		Debug.Log("Eaten, +" + food.heal + " hearth, +" + food.satiety + " satiety");
	}
}
