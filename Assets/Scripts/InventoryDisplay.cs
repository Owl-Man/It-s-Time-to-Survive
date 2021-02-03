using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
	public Food food;

	public Text name;
	public Text description;
	public Text heal;
	public Text satiety;

	public Image spriteItem;

	private void Start() 
	{
		name.text = food.name;
		description.text = food.description;
		heal.text = food.heal.ToString();
		satiety.text = food.satiety.ToString();

		spriteItem.sprite = food.sprite;

	}
}
