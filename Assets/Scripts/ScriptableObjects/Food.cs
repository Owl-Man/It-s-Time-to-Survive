using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class Food : ScriptableObject
{
	public new string name;
	public string description;
	public int heal;
	public int satiety; //Сытость

	public Sprite sprite;
}