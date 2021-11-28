using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private MessageSystem messageSystem;

    [SerializeField] private string message;

    [SerializeField] private string targetTag = "Player";

    [SerializeField] private float timeMessageActive = 1f;
    
    private bool _isMessageActivated;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (_isMessageActivated == false && other.CompareTag(targetTag)) 
        {
            _isMessageActivated = true;
            if (messageSystem != null) messageSystem.SendMessage(message, timeMessageActive);
        }
    }
}
