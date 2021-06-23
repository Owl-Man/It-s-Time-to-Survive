using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    public GameObject MessageBox;

    public Text MessageText;

    public string Message;

    public bool isMessageActivated;

    public string TargetTag = "Player";

    public float TimeMessageActive = 1f;

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag(TargetTag) && isMessageActivated == false) 
        {
            StartCoroutine(MessageAppearing());
        }
    }

    IEnumerator MessageAppearing() 
    {
        MessageBox.SetActive(true);
        MessageText.text = Message;
        isMessageActivated = true;
        yield return new WaitForSeconds(TimeMessageActive);
        MessageBox.SetActive(false);
    }
}
