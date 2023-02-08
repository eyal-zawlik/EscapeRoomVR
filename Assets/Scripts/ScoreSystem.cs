using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ScoreSystem : MonoBehaviour
{
    [Header("Reference")] [SerializeField] private Transform scoreContainer;
    
    [Header("Prefabs")] [SerializeField] private GameObject scoreBoxPrefab;

    [Header("Box EvenOdd Color")] [SerializeField] private Color even;
    [SerializeField] private Color odd;

    public ScoreLeaderBoard _leaderBoard;

    private void Awake()
    {
        even.a = 127f;
        odd.a = 127f;
    }

    private void Start()
    {
        Load();
        // setup leaderboard
        LoadDataInContainer();
    }

    private void LoadDataInContainer()
    {
        if (_leaderBoard.Records == null) return;
        _leaderBoard.SortByScore();
        foreach (var leaderBoardRecord in _leaderBoard.Records)
        {
            AddScoreBox(leaderBoardRecord);
        }
    }


    public void AddScoreBox(ScoreRecord record)
    {
        if (string.IsNullOrEmpty(record.PlayerName)) return;
        var box = Instantiate(scoreBoxPrefab).GetComponent<Image>();
        box.transform.SetParent(scoreContainer);
        var tmpText = box.GetComponentsInChildren<TMP_Text>();
        tmpText[0].text = record.PlayerName;
        tmpText[1].text = $"{record.Score}".PadLeft(3, '0');
        var rectTrans = (RectTransform)box.transform;
        rectTrans.localPosition = Vector3.zero;
        rectTrans.localScale = Vector3.one;
        rectTrans.localRotation = Quaternion.identity;
        box.color = scoreContainer.childCount % 2 == 0 ? even : odd;
    }

    public void Clear()
    {
        for (var i = 0; i < scoreContainer.childCount; i++)
        {
            DestroyImmediate(scoreContainer.GetChild(i).gameObject);
        }

        _leaderBoard.Records = new []{ new ScoreRecord()};
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetString("LeaderBoard", _leaderBoard.ToJsonString());
        // LocalDatabase.SaveJsonDataAsync(_leaderBoard,LocalDatabase.persistentDataPath, "LeaderBoard.json");
    }

    public void TestData()
    {
        var list = new List<ScoreRecord>();
        for (var i = 0; i < 5; i++)
        {
            list.Add(new ScoreRecord
            {
                PlayerName = "TEST " + i,
                Score = Random.Range(1, 300),
            });
            Debug.Log(list[i].ToJsonString());
        }

        _leaderBoard = new ScoreLeaderBoard
        {
            Records = list.ToArray()
        };
        LoadDataInContainer();
        Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("LeaderBoard"))
        {
            var value = PlayerPrefs.GetString("LeaderBoard");
            Debug.Log(value);
            _leaderBoard = JsonConvert.DeserializeObject<ScoreLeaderBoard>(value);
        }
        // LocalDatabase.LoadJsonDataAsync<ScoreLeaderBoard>(LocalDatabase.persistentDataPath, "LeaderBoard.json", value =>
        // {
        //     _leaderBoard = value;
        //     Debug.Log(value.ToJsonString());
        //     foreach (var leaderBoardRecord in _leaderBoard.Records)
        //     {
        //         AddScoreBox(leaderBoardRecord);
        //     }
        // });
    }
}

public struct ScoreRecord
{
    public string PlayerName;
    public int Score;


    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }
}

public struct ScoreLeaderBoard
{
    public ScoreRecord[] Records;

    public string ToJsonString()
    {
        return JsonConvert.SerializeObject(this);
    }
    
    public void SortByScore()
    {
        Array.Sort(Records, (x, y) => y.Score.CompareTo(x.Score));
    }
}