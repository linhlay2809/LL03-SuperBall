using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int currentScore = 0;
    protected bool _isGameOver = false;

    protected AudioSource audioS;
    [SerializeField] protected AudioClip triggerSound;
    [SerializeField] protected AudioClip gameOverSound;
    /// <summary>
    /// Singleton
    /// </summary>
    private static GameManager instance;
    public static GameManager Instance => instance;
    // tạo event updateScore
    public UnityEvent<int> updateScore;
    public bool IsGameOver 
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }
    private void Awake()
    {
        instance = this;
        audioS = GetComponent<AudioSource>();
    }
    
    public void AddScore(int score)
    {
        this.currentScore += score;
        updateScore?.Invoke(this.currentScore); // gọi sự kiện updateScore khi được gọi
        UIManager.Instance.SetScoreAnim();
        if (audioS != null)
            audioS.PlayOneShot(triggerSound);
    }
    // Lấy giá trị score
    public int GetScore()
    {
        return this.currentScore;
    }

    public void GameOver()
    {
        audioS.PlayOneShot(gameOverSound);
    }
}
