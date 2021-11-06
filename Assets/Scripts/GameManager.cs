using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    private static GameManager instance;
    public static GameManager Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public UnityEvent<int> updateScore;
    public void AddScore(int score)
    {
        this.score += score;
        updateScore?.Invoke(this.score);

    }
}
