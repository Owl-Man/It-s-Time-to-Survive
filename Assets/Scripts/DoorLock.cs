using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public GameObject DoorLocker;

    public LinkManager links;

    public string Message = "Mini-Boss Archon";

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            DoorLocker.SetActive(true);

            if (links != null) 
            {
                links.MessagePanel.SetActive(true);
                links.MessageText.text = Message;
            }

            Destroy(gameObject);
        }
    }
}
