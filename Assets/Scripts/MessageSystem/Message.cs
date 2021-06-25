using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private MessageSystem messageSystem;

    [SerializeField] private string message;

    private bool isMessageActivated;

    [SerializeField] private string targetTag = "Player";

    [SerializeField] private float timeMessageActive = 1f;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (isMessageActivated == false && other.CompareTag(targetTag)) 
        {
            isMessageActivated = true;
            if (messageSystem != null) messageSystem.SendMessage(message, timeMessageActive);
        }
    }
}
