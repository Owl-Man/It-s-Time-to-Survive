using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
	public Food food;

	public Text name;
	public Text description;
	public Text heal;
	public Text satiety;

	public Image spriteItem;
	public Image descriptionSpriteItem;

	private void Update() 
	{
		name.text = food.name;
		description.text = food.description;
		heal.text = "RevivalLifes: " + food.heal.ToString();
		satiety.text = "Satiety: " + food.satiety.ToString();

		spriteItem.sprite = food.sprite;
		descriptionSpriteItem.sprite = food.sprite;
	}
}
