using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMain : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Instance.IsGameOver = true;
            col.GetComponent<Player>().SetGameOver();
            UIManager.Instance.GameOver();
        }
    }
}
