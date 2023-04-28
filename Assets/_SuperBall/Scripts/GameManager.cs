using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int _currentScore = 0;
    private bool _isGameOver = false;

    private AudioSource _audioS;
    [SerializeField] protected AudioClip triggerSound;
    [SerializeField] protected AudioClip gameOverSound;
    /// <summary>
    /// Singleton
    /// </summary>
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    // tạo event updateScore
    public UnityEvent<int> updateScore;
    public bool IsGameOver 
    {
        get => _isGameOver;
        set => _isGameOver = value;
    }
    private void Awake()
    {
        _instance = this;
        _audioS = GetComponent<AudioSource>();
    }
    
    public void AddScore(int score)
    {
        this._currentScore += score;
        updateScore?.Invoke(this._currentScore); // gọi sự kiện updateScore khi được gọi
        UIManager.Instance.SetScoreAnim();
        if (_audioS != null)
            _audioS.PlayOneShot(triggerSound);
    }
    // Lấy giá trị score
    public int GetScore()
    {
        return this._currentScore;
    }

    public void GameOver()
    {
        this._isGameOver = true;
        _audioS.PlayOneShot(gameOverSound);
    }
}
