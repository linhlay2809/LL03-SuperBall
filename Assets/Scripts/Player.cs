using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private TargetManager targetManager;
    private GameObject targetParent;
    [SerializeField] private List<GameObject> targetList;
    [SerializeField] private List<GameObject> targetToDestroyList;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isActiveBall = false;
    [SerializeField] private float minX; 
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private void Awake()
    {
        targetParent = GameObject.Find("TargetList");
        target = targetList[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isActiveBall = true;
        }
        if (_isActiveBall)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
        }
            
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Target"))
        {
            _isActiveBall = false;
            AddTargetToDestroyList();
            SetTarget(0);
            SpawnNewTarget();
        }
    }
    void AddTargetToDestroyList()
    {
        targetToDestroyList.Add(targetList[0]);
        targetList.RemoveAt(0);
        if (targetToDestroyList.Count - 1 == 1)
        {
            Destroy(targetToDestroyList[0]);
            targetToDestroyList.RemoveAt(0);
        }
    }
    void SpawnNewTarget()
    {
        var newTarget = Instantiate(targetPrefab, SpawnRandomPos(), targetPrefab.transform.rotation);
        newTarget.transform.parent = targetParent.transform;
        targetList.Add(newTarget);
    }
    private Vector3 SpawnRandomPos()
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY) + targetList[targetList.Count - 1].transform.position.y, 0) ;
    }
    void SetTarget(int newTarget)
    {
        target = targetList[newTarget];
    }
}
