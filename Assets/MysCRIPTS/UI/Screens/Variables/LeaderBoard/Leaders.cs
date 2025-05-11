using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using _Project.Scripts;
using _Project.Scripts.Helpers;
using _Project.Scripts.Json;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Leaders : BasicScreen
{
    [SerializeField] private TMP_Text[] leadersFromLeadresName;
    [SerializeField] private TMP_Text[] leadersToLeadresScore;

    [SerializeField] private Transform _playerContainer;
    [SerializeField] private User _playerPanelPref;

    private TextManager _textManager = new TextManager();
    private List<User> _players = new List<User>();

    private int currentScore;
    private string currentName;

    [System.Serializable]
    public class PlayerDataLeader
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<PlayerDataLeader> players = new List<PlayerDataLeader>();
    }

    [SerializeField] private LeaderboardData _loadedData;

    private string filePath => Path.Combine(Application.persistentDataPath, "leaderboard.json");

    void Start()
    {
        if (!File.Exists(filePath))
        {
            GenerateFakeLeaderboard();
        }
        string jsonData = File.ReadAllText(filePath);
        _loadedData = JsonUtility.FromJson<LeaderboardData>(jsonData);
    }

    void GenerateFakeLeaderboard()
    {
        LeaderboardData data = new LeaderboardData();

        for (int i = 0; i < 128; i++)
        {
            data.players.Add(new PlayerDataLeader
            {
                playerName = GenerateRandomName(),
                score = UnityEngine.Random.Range(10, 100000)
            });
        }

        // Сортуємо за рахунком (за спаданням)
        data.players.Sort((a, b) => b.score.CompareTo(a.score));

        // Записуємо в JSON файл
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);

        Debug.Log("Фейковий лідерборд створено: " + filePath);
    }

    string GenerateRandomName()
    {
        string[] names = { "Alex", "Sam", "Chris", "Taylor", "Jordan", "Casey", "Riley", "Jamie", "Morgan", "Drew" };
        string[] adjectives = { "Fast", "Clever", "Brave", "Mighty", "Swift", "Wise", "Fierce", "Gentle", "Lucky", "Cool" };

        return adjectives[UnityEngine.Random.Range(0, adjectives.Length)] + names[UnityEngine.Random.Range(0, names.Length)] + UnityEngine.Random.Range(1, 100);
    }
    public override void SetScreen()
    {
        PlayerData.Amount.ObserveEveryValueChanged(_ => _.Value)
            .Subscribe(SetScore)
            .AddTo(this);
        for (int i = 0; i < _loadedData.players.Count; i++)
        {
            if (_loadedData.players[i].score == currentScore)
            {

                _loadedData.players.Remove((_loadedData.players[i]));
            }
        }
        
        _loadedData.players.Add(new PlayerDataLeader
        {
            playerName = currentName,
            score = currentScore,
        });

        _loadedData.players.Sort((a, b) => b.score.CompareTo(a.score));

        SetMainLeaders();
        SetLeaders();
    }

    public override void ResetScreen()
    {


        foreach (var player in _players)
        {
            Destroy(player);
        }
        _players.Clear();
    }

    private void SetScore(decimal amount)
    {
        currentScore = (int)Math.Round(amount);
        currentName = PlayerPrefs.GetString("Name", "User_Name");
    }

    private void SetLeaders()
    {
        ResetScreen();

        for (int i = 0; i < _loadedData.players.Count; i++)
        {
            User user = Instantiate(_playerPanelPref, _playerContainer);
            user._userName.text =  (i + 1) + ". " + _loadedData.players[i].playerName;
            user._coin.text = _loadedData.players[i].score.ToString();

            if(_loadedData.players[i].score == currentScore)
            {
                user._userName.color = Color.yellow;
                user._bg.SetActive(true);
            }
        }
    }

    private void SetMainLeaders()
    {
        for(int i = 0;i < leadersFromLeadresName.Length; i++)
        {
            leadersFromLeadresName[i].text = _loadedData.players[i].playerName;
            _textManager.SetText(_loadedData.players[i].score, leadersToLeadresScore[i], true);
        }
    }
}
