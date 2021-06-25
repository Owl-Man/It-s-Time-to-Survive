using UnityEngine;

public class BossFightSystem : MonoBehaviour
{
    [SerializeField] private GameObject DoorLocker;

    [SerializeField] private MessageSystem messageSystem;

    public void Victory() 
    {
        if (DoorLocker != null) DoorLocker.SetActive(false);

        if (messageSystem != null) messageSystem.SendMessage("Archon dead, you win", 2f);

        Debug.Log("Win");
    }
}
