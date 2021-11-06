using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target1 : MonoBehaviour
{
    [SerializeField] private int score;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(score);
        }
    }
}
