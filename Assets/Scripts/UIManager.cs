using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected GameObject gameOverUI;
    [SerializeField] protected GameObject scoreUI;
    [SerializeField] protected Text scoreFinal;
    [SerializeField] protected Player player;
    /// <summary>
    /// Singleton
    /// </summary>
    private static UIManager instance;
    public static UIManager Instance => instance;
    private void Awake()
    {
        instance = this;
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.IsGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        scoreFinal.text = GameManager.Instance.GetScore().ToString();
    }
}
