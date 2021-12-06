using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> targetList;
    [SerializeField] protected List<GameObject> targetToDestroyList;
    [SerializeField] protected GameObject[] targetPrefab;
    [SerializeField] protected Vector2 boundX;
    [SerializeField] protected Vector2 boundY;
    [SerializeField] protected List<int> scoreToNextLevel = new List<int>();
    protected int level = 0;
    protected GameObject targetParent;

    private void Awake()
    {
        targetParent = GameObject.Find("TargetList");
    }
    public void AddTargetToDestroyList()
    {
        targetToDestroyList.Add(targetList[0]);
        targetList.RemoveAt(0);
        if (targetToDestroyList.Count - 1 == 1)
        {
            Destroy(targetToDestroyList[0]);
            targetToDestroyList.RemoveAt(0);
        }
    }
    // sinh ra mục tiêu mới
    public void SpawnNewTarget()
    {
        var newTarget = Instantiate(targetPrefab[RandomTarget()], SpawnRandomPos(), Quaternion.Euler(0,0,Random.Range(0,360)));
        newTarget.transform.parent = targetParent.transform;
        targetList.Add(newTarget);
    }
    // Random vị trí spawn mới của mục tiêu
    private Vector3 SpawnRandomPos()
    {
        return new Vector3(Random.Range(boundX.x, boundX.y), Random.Range(boundY.x, boundY.y) + targetList[targetList.Count - 1].transform.position.y, 0);
    }
    // Get gameobject mực tiêu
    public GameObject GetTarget(int targetIndex)
    {
        return targetList[targetIndex];
    }
    // Ramdom mục tiêu trong list targetPrefab
    int RandomTarget()
    {
        if (level + 1 == targetPrefab.Length)
            return Random.Range(0, level + 1);
        if (GameManager.Instance.GetScore() > scoreToNextLevel[level])
            level += 1;
        return Random.Range(0, level + 1);
    }
}
