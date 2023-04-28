using UnityEngine;

namespace Target_Scripts
{
    public class ObstacleMain : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected Vector2 minMaxSpeed;
        [SerializeField] protected GameObject parent;
        // Start is called before the first frame update
        private void Start()
        {
            parent = gameObject.transform.parent.gameObject;
            speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y); // Random speed cho Obstacle con
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                GameManager.Instance.GameOver();
                col.GetComponent<Player>().GameOver();
                UIManager.Instance.GameOver();
            }
        }
    }
}
