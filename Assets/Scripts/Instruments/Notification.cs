using Unity.Notifications.Android;
using UnityEngine;

namespace Instruments
{
    public class Notification : MonoBehaviour
    {
        private bool _isPaused;

        private int _timeForNotification;

        [SerializeField] private string GameName, NotificationTitle, NotificationText;

        private void Start() 
        {
            if (PlayerPrefs.GetInt("isChannelCreated") != 1) CreateNotificationChannel();
        }

        private void OnApplicationPause (bool pauseStatus) 
        {
            _isPaused = pauseStatus;

            if (_isPaused) TrySendNotification();
        }

        private void OnApplicationQuit () => TrySendNotification();

        public void TrySendNotification()
        {
            int isSending = Random.Range(1, 6); //Шанс отправки уведомления

            if (isSending == 1) SendNotification();
        }

        public void CreateNotificationChannel() 
        {
            PlayerPrefs.SetInt("isChannelCreated", 1);

            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = GameName,
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        public void SendNotification() 
        {
            var notification = new AndroidNotification();
            notification.Title = NotificationTitle;
            notification.Text = NotificationText;
            notification.LargeIcon = "icon_0";
            notification.SmallIcon = "icon_1";
            _timeForNotification = Random.Range(550, 2600);
            notification.FireTime = System.DateTime.Now.AddMinutes(_timeForNotification);

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
