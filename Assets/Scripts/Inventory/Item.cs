using UnityEngine;

namespace Inventory
{
	public class Item : MonoBehaviour
	{
		public int id;
		public bool isItemSelected;
		public string item;
	
		public int MaxStackCountInSlot;
	}
}