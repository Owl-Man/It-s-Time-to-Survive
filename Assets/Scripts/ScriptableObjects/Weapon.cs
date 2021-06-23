using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject
{
	public new string name;
	public string description;
	public int damage;
	public string rare;
	
	public Sprite sprite;
}
