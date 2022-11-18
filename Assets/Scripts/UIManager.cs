using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected GameObject gameOverUI;
    [SerializeField] protected GameObject scoreUI;
    [SerializeField] protected TextMeshProUGUI scoreFinaltxt;
    [SerializeField] protected TextMeshProUGUI highScoretxt;
    [SerializeField] protected TextMeshProUGUI finalHighScoretxt;
    [SerializeField] protected Player player;
    protected Animator anim;
    private int currentHighScore;
    /// <summary>
    /// Singleton
    /// </summary>
    private static UIManager instance;
    public static UIManager Instance => instance;
    private void Awake()
    {
        instance = this;
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(SetHighScore());
        // Invoke("SetHighScore", 1f);
    }

    private IEnumerator SetHighScore()
    {
        yield return new WaitForSeconds(2f);
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "Rank",
            StartPosition = 0,
            MaxResultsCount = 1,
        }, resultCallback =>
        {
            currentHighScore = resultCallback.Leaderboard[0].StatValue;
            highScoretxt.SetText(currentHighScore.ToString());
        }, errorCallback =>
        {
            Debug.Log(errorCallback.Error);
        });
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
    public void GameOver()
    {
        int score;
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        score = GameManager.Instance.GetScore();
        scoreFinaltxt.text = score.ToString();
        PlayfabManager.Instance.SendLeaderboard(score);
        if (score > currentHighScore)
        {
            currentHighScore = score;
        }

        finalHighScoretxt.SetText(currentHighScore.ToString());
    }
    // Active score animator when trigger target
    public void SetScoreAnim()
    {
        anim.SetTrigger("Trigger");
    }
}
