using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float posY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            var position = transform.position;
            Vector3 newPos = new Vector3(0, player.transform.position.y + posY, position.z);
            position = Vector3.Lerp(position, newPos, 2f);
            transform.position = position;
        }
    }
    
}
