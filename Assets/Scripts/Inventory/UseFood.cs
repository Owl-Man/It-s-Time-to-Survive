using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFood : MonoBehaviour
{
	public Food food;
	private PlayerController player;

	private void Start() 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	public void EatFood() 
	{
		player.health += food.heal;
		player.satiety += food.satiety;

		Debug.Log("Eaten, +" + food.heal + " hearth, +" + food.satiety + " satiety");
	}
}
