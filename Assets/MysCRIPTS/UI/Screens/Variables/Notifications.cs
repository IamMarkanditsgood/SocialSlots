using Unity.Notifications.iOS;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notifications : MonoBehaviour
{
    public Button notifications;
    public Button later;

    void Start()
    {
        notifications.onClick.AddListener(NotificationsPressed);
        later.onClick.AddListener(Later);
    }
    private void OnDestroy()
    {
        notifications.onClick.RemoveListener(NotificationsPressed);
        later.onClick.RemoveListener(Later);
    }

    private void NotificationsPressed()
    {
        gameObject.SetActive(false);
        StartCoroutine(RequestAuthorization());
    }
    private void Later()
    {
        gameObject.SetActive(false);
    }
    private IEnumerator RequestAuthorization()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            }

            if (req.Granted)
            {
                PlayerPrefs.SetInt("Notifications", 1);
                Debug.Log("Сповіщення дозволені користувачем.");
            }
            else
            {
                Debug.LogWarning("Користувач не дозволив сповіщення.");
            }
        }
    }
}