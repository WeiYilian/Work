using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    //刷新范围
    private float Range = 5;

    private float mageOdd = 0.3f;

    private int mageNumber;
    
    private int warriorNumber;
    
    private int maxMageNumber = 1;
    
    private int maxWarriorNumber = 3; 
    //刷新间隔
    public float intervalTime = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(RandomSpawnEnemy),0f,intervalTime);
    }

    public void RandomSpawnEnemy()
    {
        bool isMage = Random.value < mageOdd;
        if (isMage)
        {
            if (maxMageNumber <= mageNumber) return;
            GameObject go = ObjectPool.Instance.Get("Mage");
            go.transform.position = RandomSpawnPoint();
            mageNumber++;
        }
        else
        {
            if (maxWarriorNumber <= warriorNumber) return;
            GameObject go = ObjectPool.Instance.Get("Warrior");
            go.transform.position = RandomSpawnPoint();
            warriorNumber++;
        }
    }
    
    /// <summary>
    /// 随机刷新点
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomSpawnPoint()
    {
        Vector3 randomSpawnPoint = Vector3.zero;
       
        float randomX = Random.Range(-Range, Range);
        float randomZ = Random.Range(-Range, Range);

        var position = transform.position;
        Vector3 randomPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);
        
        NavMeshHit hit;
        randomSpawnPoint =  NavMesh.SamplePosition(randomPoint, out hit, Range, 1) ? hit.position : Vector3.zero;
        
        return randomSpawnPoint;
    }
}
