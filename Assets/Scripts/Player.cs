using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected TargetManager targetManager;
    [SerializeField] protected GameObject target;
    [SerializeField] protected GameObject triggerEffect;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected float _speed;
    [SerializeField] protected bool _isActiveBall = false;

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
        if (GameManager.Instance.IsGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isActiveBall = true;
            }
            if (_isActiveBall)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
                if (transform.position == target.transform.position)
                {
                    _isActiveBall = false;
                    AfterTriggerTarget();
                }
            }
        }

    }
    // Kích hoạt khi player cùng vị trí với target
    void AfterTriggerTarget()
    {
        SpawnEffect();
        targetManager.SpawnNewTarget();
        targetManager.AddTargetToDestroyList();
        SetTarget(0);
    }
    // code gán new target
    void SetTarget(int newTarget)
    {
        target = targetManager.GetTarget(newTarget);
    }
    // Spawn VFX khi trigger point mới
    void SpawnEffect()
    {
        GameObject triggerEff = Instantiate(triggerEffect, target.transform.position, triggerEffect.transform.rotation);
        Destroy(triggerEff, 1);
    }
    public void SetGameOver()
    {
        GameObject deathEff = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        Destroy(deathEff, 1);
        Destroy(gameObject);
    }
}
