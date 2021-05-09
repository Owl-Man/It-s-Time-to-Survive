using UnityEngine;
public class PickUp : LinkManager
{
    private InventorySystem inventory;
    public GameObject slotButton; // item in slot
    

    private GameObject child;

    private void Start() => inventory = ManagerInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                inventory.slotScripts[i].GetChild();
                child = inventory.slotScripts[i].Child;

				if (child != null && slotButton.GetComponent<Item>().item == child.GetComponent<Item>().item) 
				{
					inventory.slotScripts[i].CountOfItems++;
                    
                    break;
				}

                if (inventory.isFull[i] == false)
                {
					AddItem(i);
                    break;
                }
            }
        }
    }

    private void AddItem(int i)
    {
        inventory.isFull[i] = true;

        GameObject PickUpedItem = Instantiate(slotButton, inventory.slots[i].transform);

        PickUpedItem.GetComponent<Item>().id = i;

        inventory.slotScripts[i].GetChild();

        inventory.slotScripts[i].isSlotHaveItem = true;

        inventory.slotScripts[i].CountOfItems++;

        Destroy(gameObject);
    }
}
