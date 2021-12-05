using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetList;
    [SerializeField] private List<GameObject> targetToDestroyList;
    [SerializeField] private GameObject[] targetPrefab;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    private GameObject targetParent;

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
    public void SpawnNewTarget()
    {
        var newTarget = Instantiate(targetPrefab[RandomTarget()], SpawnRandomPos(), Quaternion.Euler(0,0,Random.Range(0,360)));
        newTarget.transform.parent = targetParent.transform;
        targetList.Add(newTarget);
    }
    private Vector3 SpawnRandomPos()
    {
        return new Vector3(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY) + targetList[targetList.Count - 1].transform.position.y, 0);
    }
    public GameObject GetTarget(int targetIndex)
    {
        return targetList[targetIndex];
    }
    int RandomTarget()
    {
        return Random.Range(0, targetPrefab.Length);
    }
}
