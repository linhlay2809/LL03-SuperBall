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
    protected Animator anim;
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
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        scoreFinal.text = GameManager.Instance.GetScore().ToString();
    }
    // Active score animator when trigger target
    public void SetScoreAnim()
    {
        anim.SetTrigger("Trigger");
    }
}
