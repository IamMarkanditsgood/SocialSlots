using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Leaders : BasicScreen
{
    [SerializeField] private TMP_Text[] leadersFromLeadresName;
    [SerializeField] private int[] leadersToLeadresScore;

    [SerializeField] private Transform _playerContainer;
    [SerializeField] private PlayerPanel _playerPanelPref;

    private List<PlayerPanel> _players = new List<PlayerPanel>();

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<PlayerData> players = new List<PlayerData>();
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
            data.players.Add(new PlayerData
            {
                playerName = GenerateRandomName(),
                score = Random.Range(10, 100000)
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

        return adjectives[Random.Range(0, adjectives.Length)] + names[Random.Range(0, names.Length)] + Random.Range(1, 100);
    }
    public override void SetScreen()
    {
        SetMainLeaders();
        SetLeaders();
    }

    public override void ResetScreen()
    {
        foreach(var player in _players)
        {
            Destroy(player);
        }
        _players.Clear();
    }

    private void SetLeaders()
    {

    }

    private void SetMainLeaders()
    {
        for(int i = 0;i < leadersFromLeadresName.Length; i++)
        {
            leadersFromLeadresName[i].text = _loadedData.players[i].playerName;
            leadersToLeadresScore[i] = _loadedData.players[i].score;
        }
    }
}
