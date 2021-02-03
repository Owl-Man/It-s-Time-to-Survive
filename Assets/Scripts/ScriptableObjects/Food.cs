using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Food : ScriptableObject
{
	public new string name;
	public string description;
	public int heal;
	public int satiety; //Сытость

	public Sprite sprite;
}
