using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : BasicScreen
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _getBonus;

    [SerializeField] private GameObject _factPanel;
    [SerializeField] private GameObject _timerPanel;

    [SerializeField] private TMP_Text _timer;

    private const string LastClaimTimeKey = "LastClaimTime";
    private TimeSpan rewardCooldown = TimeSpan.FromHours(24);


    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
        _okButton.onClick.AddListener(BackButton);
        _getBonus.onClick.AddListener(GiveBonus);
    }

    private void OnDestroy()
    {
        _backButton.onClick.RemoveListener(BackButton);
        _okButton.onClick.RemoveListener(BackButton);
        _getBonus.onClick.RemoveListener(GiveBonus);
    }

    private void Update()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;
        _timer.text = $"{timeRemaining.Hours:D2}:{timeRemaining.Minutes:D2}:{timeRemaining.Seconds:D2}";
    }


    public override void ResetScreen()
    {

    }

    public override void SetScreen()
    {
        DateTime lastClaimTime = GetLastClaimTime();
        DateTime nextClaimTime = lastClaimTime + rewardCooldown;
        TimeSpan timeRemaining = nextClaimTime - DateTime.Now;

        if (timeRemaining <= TimeSpan.Zero)
        {
            _factPanel.SetActive(true);
            _timerPanel.SetActive(false);
        }
        else
        {
            
            _factPanel.SetActive(false);
            _timerPanel.SetActive(true);
        }


    }

    private void GiveBonus()
    {
        int newScore = PlayerPrefs.GetInt("Score");
        newScore += 700;
        PlayerPrefs.SetInt("Score", newScore);

        PlayerPrefs.SetString(LastClaimTimeKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
        SetScreen();
    }

    private void BackButton()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private DateTime GetLastClaimTime()
    {
        string lastClaimStr = PlayerPrefs.GetString(LastClaimTimeKey, string.Empty);
        return string.IsNullOrEmpty(lastClaimStr) ? DateTime.MinValue : DateTime.Parse(lastClaimStr);
    }
}