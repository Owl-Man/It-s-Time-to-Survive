using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private GameObject DoorLocker;

    [SerializeField] private MessageSystem messageSystem;

    [SerializeField] private float messageTime;

    [SerializeField] private string message = "Mini-Boss Archon";

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            DoorLocker.SetActive(true);

            if (messageSystem != null) messageSystem.SendMessage(message, 2f);

            Destroy(gameObject);
        }
    }
}
