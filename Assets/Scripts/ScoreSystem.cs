using Database;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
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
        LocalDatabase.SaveJsonDataAsync(_leaderBoard,LocalDatabase.persistentDataPath, "LeaderBoard.json");
    }

    public void Load()
    {
        LocalDatabase.LoadJsonDataAsync<ScoreLeaderBoard>(LocalDatabase.persistentDataPath, "LeaderBoard.json", value =>
        {
            _leaderBoard = value;
            Debug.Log(value.ToJsonString());
            foreach (var leaderBoardRecord in _leaderBoard.Records)
            {
                AddScoreBox(leaderBoardRecord);
            }
        });
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
        return JsonConvert.SerializeObject(Records);
    }
}