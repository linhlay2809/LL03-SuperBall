using System.Collections.Generic;
using UnityEngine;

namespace Target_Scripts
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField] protected List<Target> targetList;
        [SerializeField] protected List<Target> targetToDestroyList;
        [SerializeField] protected Target[] targetPrefab;
        [SerializeField] protected Vector2 boundX;
        [SerializeField] protected Vector2 boundY;
        [SerializeField] protected List<int> scoreToNextLevel = new List<int>();
        [SerializeField] protected int level = 0;
        private GameObject _targetParent;

        private void Awake()
        {
            _targetParent = GameObject.Find("TargetList");
        }
        // Add target đã trigger vào destroyList
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
            newTarget.transform.parent = _targetParent.transform;
            targetList.Add(newTarget);
        }
        // Random vị trí spawn mới của mục tiêu
        private Vector3 SpawnRandomPos()
        {
            return new Vector3(Random.Range(boundX.x, boundX.y), Random.Range(boundY.x, boundY.y) + targetList[targetList.Count - 1].transform.position.y, 0);
        }
        // Get gameobject mục tiêu
        public Target GetTarget(int targetIndex)
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
}
