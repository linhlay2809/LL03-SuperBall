using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_1 : ObstacleMain
{
    private void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
