using UnityEngine;

public class BossFightSystem : MonoBehaviour
{
    public GameObject DoorLocker;

    public LinkManager links;

    public void Victory() 
    {
        if (DoorLocker != null) DoorLocker.SetActive(false);

        if (links != null) 
        {
            links.MessagePanel.SetActive(true);
            links.MessageText.text = "Archon dead, you win";
        }

        Debug.Log("Win");
    }
}
