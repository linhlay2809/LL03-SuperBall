﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextScore : MonoBehaviour
{
    private TextMeshProUGUI _textScore;
    private void Awake()
    {
        _textScore = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        AddScore(0);
        GameManager.Instance.updateScore.AddListener(AddScore); // Lắng nghe event updateScore
    }

    private void AddScore(int score)
    {
        _textScore.text = score.ToString(); // Cập nhật score lên UI
    }

   
}
