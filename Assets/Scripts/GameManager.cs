using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int currentScore = 0;

    protected bool _isGameOver = false;

    private static GameManager instance;
    public static GameManager Instance => instance;
    public bool IsGameOver 
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }
    private void Awake()
    {
        instance = this;
    }
    // tạo event updateScore
    public UnityEvent<int> updateScore;
    public void AddScore(int score)
    {
        this.currentScore += score;
        updateScore?.Invoke(this.currentScore); // gọi sự kiện updateScore khi được gọi

    }
    public int GetScore()
    {
        return this.currentScore;
    }
}
