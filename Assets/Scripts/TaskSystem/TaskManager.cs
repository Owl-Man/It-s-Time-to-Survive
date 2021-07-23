using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private GameObject TaskPanel;

    [SerializeField] private Text TaskHeader;

    [SerializeField] private Text TaskHeaderInFull;

    [SerializeField] private Text TaskDescription;

    public int CompletedTask;

    private const int allTasksCompleted = -1;

    [SerializeField] private int MaxTasks;

    [SerializeField] private Task[] tasks;

    private void Awake() 
    {
        CompletedTask = PlayerPrefs.GetInt("CompletedTask");

        UpdateTask();
    }

    public void TaskComplete() 
    {
        CompletedTask++;

        if (CompletedTask >= MaxTasks)
        {
            AllTasksComplete();

            CompletedTask = allTasksCompleted;
            PlayerPrefs.SetInt("CompletedTask", CompletedTask);
            return;
        }

        PlayerPrefs.SetInt("CompletedTask", CompletedTask);

        UpdateTask();
    }

    public void OnTaskButtonClick() => TaskPanel.SetActive(true);

    public void OnBackForTaskButtonClick() => TaskPanel.SetActive(false);

    private void UpdateTask() 
    {
        if (CompletedTask == allTasksCompleted) 
        {
            AllTasksComplete();
            return;
        }

        TaskHeader.text = "Task: " + tasks[CompletedTask].task;

        if (TaskHeaderInFull != null) TaskHeaderInFull.text = tasks[CompletedTask].task;
        if (TaskDescription != null) TaskDescription.text = tasks[CompletedTask].taskDescription;
    }


    private void AllTasksComplete() 
    {
        TaskHeader.text = "Task: Survive";

        if (TaskHeaderInFull != null) TaskHeaderInFull.text = "Survive";
        if (TaskDescription != null) TaskDescription.text = "Survive and explore the island further!  And remember that every 7 days a large wave of monsters will come.  This night will be the red moon and possibly the last.";
    }
}