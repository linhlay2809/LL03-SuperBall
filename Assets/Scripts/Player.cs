﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected TargetManager targetManager;
    [SerializeField] protected GameObject target;
    [SerializeField] protected ParticleSystem triggerEffect;
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
            // Nhận diện thao tác chạm trên dt
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) // Khi bắt đầu chạm
                {
                    _isActiveBall = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isActiveBall = true;
            }

            if (_isActiveBall)
            {
                // Di chuyển player đến target
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
        PlayTriggerEffect();
        targetManager.SpawnNewTarget();
        targetManager.AddTargetToDestroyList();
        SetTarget(0);
    }

    private void PlayTriggerEffect()
    {
        triggerEffect.Play();
    }

    // code gán new target
    void SetTarget(int newTarget)
    {
        target = targetManager.GetTarget(newTarget);
    }
    public void GameOver()
    {
        GameObject deathEff = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        Destroy(deathEff, 1);
        Destroy(gameObject);
        AdsManager.Instance.ShowAd();
    }
}
