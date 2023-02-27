using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager instance
    {
        private set
        {
            if (_instance != null)
            {
                Debug.Log("GameManager Already Exists");
                Destroy(value.gameObject);
                return;
            }

            _instance = value;
        }
        get => _instance;
    }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Transform playerTrans;

    private void Awake()
    {
        instance = this;
        if (_spawnLocation.Count <= 0)
        {
            ValidatePuzzles();
        }

        gameWinUI.SetActive(false);
        msgUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    private void Start()
    {
        // quite action
        quitAction.Enable();
        quitAction.started += _ =>
        {
            if (_isGameOver)
            {
                Application.Quit();
            }
        };
        SpawnPuzzles();
        StartCountDown();
    }

    private void Update()
    {
        if (_hasStarted)
            Tick();
    }

    private void OnValidate()
    {
        ValidatePuzzles();
    }

    #region CountDown

    [Header("CountDown")] [SerializeField] private TMP_Text countDownTxt;
    [SerializeField] private GameObject msgUI;
    [SerializeField] private AudioClip startSoundClip;

    private void StartCountDown()
    {
        StartCoroutine(CountDown(12f, count =>
        {
            var value = 10 - count;
            countDownTxt.text = value > 0 ? $"{value}".PadLeft(2, '0') : "Let's Go!";
        }, () =>
        {
            msgUI.gameObject.SetActive(false);
            Init();
            // give starting sound effect
            AudioSource.PlayClipAtPoint(startSoundClip, playerTrans.position, 1.0f);
        }));
    }


    private IEnumerator CountDown(float countDuration, Action<int> tickAction, Action endOfIt)
    {
        var time = 0f;
        while (time < countDuration)
        {
            yield return new WaitForSeconds(1);
            time++;
            tickAction.Invoke((int)time);
        }

        endOfIt.Invoke();
    }

    #endregion

    #region Timer

    [Header("Timer")] // Duration of the timer in seconds
    public TMP_Text timerText; // Text to display the timer

    [SerializeField] private AudioSource backgroundMusic;

    private float _timeSpend; // Time left on the timer
    private bool _hasStarted;

    public void Init()
    {
        _timeSpend = 0;
        _hasStarted = true;
    }

    private void Tick()
    {
        _timeSpend += Time.deltaTime;

        var minutes = Mathf.FloorToInt(_timeSpend / 60);
        var seconds = Mathf.FloorToInt(_timeSpend % 60);
        var timeString = $"{minutes:00}:{seconds:00}";
        timerText.text = timeString;
    }

    #endregion

    #region Puzzles

    [Header("Puzzles")] [SerializeField] private GameObject[] puzzlesPrefab;
    [SerializeField] private Transform puzzlesSpawnLocationParent;
    [SerializeField] private Transform puzzlesParent;
    public PuzzleDoor puzzleDoor;

    public const int Coins = 3;

    public int solvedCoins;
    public bool foundMissingPuzzle;
    public bool clockCodeSolved;

    private List<Vector3> _spawnLocation = new List<Vector3>();

    public bool VerifyPuzzles()
    {
        return solvedCoins == 3 && foundMissingPuzzle && (_hasSpin && clockCodeSolved);
    }

    private void ValidatePuzzles()
    {
        if (puzzlesSpawnLocationParent == null) return;
        _spawnLocation = new List<Vector3>();
        for (var i = 0; i < puzzlesSpawnLocationParent.childCount; i++)
        {
            _spawnLocation.Add(puzzlesSpawnLocationParent.GetChild(i).position);
        }
    }

    private void SpawnPuzzles()
    {
        foreach (var o in puzzlesPrefab)
        {
            o.transform.position = GetRandomPos();
        }
        // foreach (var o in puzzlesPrefab)
        // {
        //     if (o.name.Equals("Coin"))
        //     {
        //         for (int i = 0; i < Coins; i++)
        //         {
        //             Instantiate(o, GetRandomPos(), Quaternion.identity, puzzlesParent);
        //         }
        //
        //         continue;
        //     }
        //
        //     Instantiate(o, GetRandomPos(), Quaternion.identity, puzzlesParent);
        // }
    }

    private Vector3 GetRandomPos()
    {
        var pos = _spawnLocation[Random.Range(0, _spawnLocation.Count)];
        _spawnLocation.Remove(pos);
        return pos;
    }

    #endregion

    #region PuzzleClock

    [Header("Clock")] [SerializeField] private float rotationSpeed;
    public int clockCode;

    [SerializeField] private Transform gear1, gear2, gear3;
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private TMP_Text codeValue;

    private bool _hasSpin;

    public void SpinClock()
    {
        // Randomly set the rotation duration between 2 and 5 seconds
        StartCoroutine(RotateGear(Random.Range(4f, 6f)));
        _hasSpin = true;
        // Play Sound Effect
        soundEffect.Play();
    }

    private IEnumerator RotateGear(float time)
    {
        var elapsed = 0f;

        // Calculate the target rotation angle for gear 1 based on the current clock value
        var targetAngle1 = rotationSpeed * 30f; // Each hour mark is 30 degrees

        // Calculate the target rotation angle for gear 2 based on the rotation angle of gear 1
        var targetAngle2 =
            -targetAngle1 * 2f; // Gear 2 rotates in the opposite direction and at double the speed of gear 1

        // Calculate the target rotation angle for gear 3 based on the rotation angle of gear 2
        var targetAngle3 = targetAngle2 / 2f; // Gear 3 rotates in the same direction and at half the speed of gear 2

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            gear1.Rotate(0, 0, targetAngle2, Space.World);
            gear2.Rotate(0, 0, targetAngle1 * Mathf.Clamp01(time - elapsed), Space.World);
            gear3.Rotate(0, 0, targetAngle3, Space.World);
            yield return null;
        }


        var xAxis = gear2.localEulerAngles.x;
        var correctAngle = (float)((int)(xAxis / 30 % 12) * 30);
        gear2.localEulerAngles = new Vector3(correctAngle, 0, 0);

        var a = (int)(correctAngle + 180) / 30 % 12;
        var b = (int)(correctAngle + 330) / 30 % 12;
        var c = (int)(correctAngle + 30) / 30 % 12;

        a = a == 0 ? 12 : a;
        b = b == 0 ? 12 : b;
        c = c == 0 ? 12 : c;

        clockCode = (a + b) * c;
        codeValue.text = clockCode+"";
    }

    #endregion

    #region GameOver

    [Header("GameOver")] [SerializeField] private GameObject gameWinUI;
    [SerializeField] private TMP_Text msgTxt;

    [SerializeField] private InputAction quitAction;
    private bool _isGameOver;
    

    public void GameOver()
    {
        _hasStarted = false;
        _isGameOver = true;
        var profile = Resources.Load<PlayerProfile>("Profile");
        var pName = string.IsNullOrEmpty(profile.playerName) ? PlayerPrefs.GetString("PlayerName") : profile.playerName;
        var score = Mathf.RoundToInt(999 / _timeSpend);
        gameWinUI.SetActive(true);
        msgTxt.text = $"Well done {pName} Your Score is \n{score}";
        ScoreLeaderBoard highScoreBoard = default;
        var highScoreList = new List<ScoreRecord>();
        if (PlayerPrefs.HasKey("LeaderBoard"))
        {
            var value = PlayerPrefs.GetString("LeaderBoard");
            highScoreBoard = JsonConvert.DeserializeObject<ScoreLeaderBoard>(value);
            highScoreList.AddRange(highScoreBoard.Records);
        }

        var record = new ScoreRecord()
        {
            PlayerName = profile.playerName,
            Score = score
        };
        highScoreList.Add(record);

        highScoreBoard.Records = highScoreList.ToArray();

        PlayerPrefs.SetString("LeaderBoard", highScoreBoard.ToJsonString());
        PlayerPrefs.Save();
    }

    #endregion
}