using System.Collections;
using System.Collections.Generic;
using Target_Scripts;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected TargetManager targetManager;
    [SerializeField] protected Target target;
    [SerializeField] protected ParticleSystem triggerEffect;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected bool isActiveBall = false;
    
    private void Start()
    {
        SetTarget(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver == false)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isActiveBall = true;
            }
#else
            // Nhận diện thao tác chạm trên dt
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) // Khi bắt đầu chạm
                {
                    isActiveBall = true;
                }
            }
#endif       

            if (isActiveBall)
            {
                // Di chuyển player đến target
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                if (transform.position == target.transform.position)
                {
                    isActiveBall = false;
                    GameManager.Instance.AddScore(target.Score);
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
    }
}
