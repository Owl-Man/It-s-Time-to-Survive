using UnityEngine;
using UnityEngine.SceneManagement;

public class Task : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;

    [SerializeField] private int TaskId;

    [SerializeField] private string WhatSceneTaskFor;

    public string task;

    public string taskDescription;

    private void Start() 
    {
        if (WhatSceneTaskFor != SceneManager.GetActiveScene().name) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (taskManager.CompletedTask + 1 == TaskId && other.CompareTag("Player")) 
        {
            taskManager.TaskComplete();

            Destroy(gameObject);
        }
    }
}