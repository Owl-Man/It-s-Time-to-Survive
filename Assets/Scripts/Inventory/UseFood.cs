using UnityEngine;

public class UseFood : MonoBehaviour
{
	public LinkManager links;
	public Food food;
	private Indicators indicators;

	private void Start() => indicators = links.indicators;

	public void EatFood() 
	{
		indicators.health += food.heal;
		indicators.satiety += food.satiety;

		Debug.Log("Eaten, +" + food.heal + " hearth, +" + food.satiety + " satiety");
	}
}
