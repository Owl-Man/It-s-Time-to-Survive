using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    [SerializeField] private GameObject MessageBox;

    [SerializeField] private Text MessageText;

    public void SendMessage(string message, float messageTime) => StartCoroutine(SendingMessage(message, messageTime));

    private IEnumerator SendingMessage(string message, float messageTime) 
    {
        MessageBox.SetActive(true);
        MessageText.text = message;
        yield return new WaitForSeconds(messageTime);
        MessageBox.SetActive(false);
    }
}
