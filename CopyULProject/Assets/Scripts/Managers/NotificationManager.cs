using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

namespace EY.Managers.Notification
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] private List<Notification> notifications;
        [SerializeField] private Image PromptImage;
        private static NotificationManager _instance;
        public static NotificationManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void ShowPromptMessage(string message, float promptDuration = 5f)
        {
            var notification = notifications.Where(n=> n.Name == message).FirstOrDefault();
            if (notification != null) StartCoroutine(ShowPrompt(notification, promptDuration));
        }

        private IEnumerator ShowPrompt(Notification notification, float duration = 5f)
        {
            PromptImage.sprite = notification.MessageSprite;
            PromptImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            PromptImage.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class Notification
    {
        public string Name;
        public Sprite MessageSprite;
    }
}

