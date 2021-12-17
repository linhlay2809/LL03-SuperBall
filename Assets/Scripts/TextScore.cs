using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextScore : MonoBehaviour
{
    private Text textScore;
    private void Awake()
    {
        textScore = GetComponent<Text>();
    }
    private void Start()
    {
        AddScore(0);
        GameManager.Instance.updateScore.AddListener(AddScore); // Lắng nghe event updateScore
    }

    private void AddScore(int score)
    {
        textScore.text = score.ToString(); // Cập nhật score lên UI
    }

   
}
