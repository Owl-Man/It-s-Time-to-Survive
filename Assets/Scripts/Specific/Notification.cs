using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class Notification : MonoBehaviour
{
    bool ispaused = false;

    int timeForNotification;

    private void Start() 
    {
        if (PlayerPrefs.GetInt("isChannelCreated") != 1) 
        {
            CreateNotificationChannel();
        }
    }

    void OnApplicationPause (bool pauseStatus) 
    {
        ispaused = pauseStatus;

        if (ispaused == true) 
        {
            TrySendNotification();
        }
    }

    void OnApplicationQuit ()
    {
        TrySendNotification();
    }

    public void TrySendNotification()
    {
        int isSending = UnityEngine.Random.Range(1, 6); //Шанс отправки уведомления

        if (isSending == 1) 
        {
            SendNotification();
        }
    }

    public void CreateNotificationChannel() 
    {
        PlayerPrefs.SetInt("isChannelCreated", 1);

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "It's time to survive",
            Importance = Importance.High,
            Description = "Generic notifications",
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification() 
    {
        var notification = new AndroidNotification();
        notification.Title = "Время выживать!";
        notification.Text = "И конечно же уничтожать врагов!";
        notification.LargeIcon = "icon_0";
        notification.SmallIcon = "icon_1";
        timeForNotification = UnityEngine.Random.Range(550, 2600);
        notification.FireTime = System.DateTime.Now.AddMinutes(timeForNotification);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
