using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private TargetManager targetManager;
    [SerializeField] private GameObject target;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isActiveBall = false;

    private void Awake()
    {
        targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();
    }
    private void Start()
    {
        SetTarget(0);
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
            targetManager.SpawnNewTarget();
            targetManager.AddTargetToDestroyList();
            SetTarget(0);
        }
    }
    void SetTarget(int newTarget)
    {
        target = targetManager.GetTarget(newTarget);
    }
}
