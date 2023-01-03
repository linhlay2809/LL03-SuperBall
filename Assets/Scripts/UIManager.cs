using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ads;
using Playfab;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private GameObject changeNamePnl;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private Button okBtn;
    
    [Header("Score")]
    [SerializeField] protected GameObject gameOverUI;
    [SerializeField] protected GameObject scoreUI;
    [SerializeField] protected TextMeshProUGUI scoreFinaltxt;
    [SerializeField] protected TextMeshProUGUI highScoretxt;
    [SerializeField] protected TextMeshProUGUI finalHighScoretxt;
    [SerializeField] protected Player player;
    private Animator _anim;
    private int _currentHighScore;
    
    /// <summary>
    /// Singleton
    /// </summary>
    private static UIManager _instance;
    public static UIManager Instance => _instance;
    private void Awake()
    {
        _instance = this;
        _anim = GetComponent<Animator>();
    }
    
    private async void Start()
    {
        await PlayfabManager.Instance.Login(changeNamePnl);
        okBtn.onClick.AddListener(ChangeName);
        await SetHighScore();
    }

    private async void ChangeName()
    {
        await PlayfabManager.Instance.UpdateDisplayName(inputFieldName.text);
        changeNamePnl.SetActive(false);
    }
    
    private async Task SetHighScore()
    {
        await Task.Delay(1500);
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "Rank",
            StartPosition = 0,
            MaxResultsCount = 1,
        }, resultCallback =>
        {
            _currentHighScore = resultCallback.Leaderboard[0].StatValue;
            highScoretxt.SetText(_currentHighScore.ToString());
        }, errorCallback =>
        {
            Debug.Log(errorCallback.Error);
        });
        await Task.Yield();
    }
    private void Update()
    {
        if (GameManager.Instance.IsGameOver == true)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // retart game when IsGameover = true
                }
            }
        }
        //#if UNITY_EDITOR //Active when play editor mode
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.IsGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
//#endif
    }
    public async void GameOver()
    {
        int score;
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        score = GameManager.Instance.GetScore();
        scoreFinaltxt.text = score.ToString();
        PlayfabManager.Instance.SendLeaderboard(score);
        if (score > _currentHighScore)
        {
            _currentHighScore = score;
        }

        finalHighScoretxt.SetText(_currentHighScore.ToString());
        await AdsManager.Instance.ShowRewardedAd();

    }
    // Active score animator when trigger target
    public void SetScoreAnim()
    {
        _anim.SetTrigger("Trigger");
    }
}
