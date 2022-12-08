using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MageAttack : MonoBehaviour,IPoolable
{
    public GameObject attackTarget;
    private CharacterStats parentStats;
    private int damage = 10;
    private float speed = 1f;

    private void Update()
    {
        AttackPlayer();
        Destroy(gameObject,3);
    }

    public void AttackPlayer()
    {
        var direction =attackTarget.transform.position+new Vector3(0,1,0) - transform.position  ;
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var targetStats = attackTarget.GetComponentInChildren<CharacterStats>();
            targetStats.TakeDamage(damage, targetStats);
            ObjectPool.Instance.Remove("MageSkill",gameObject);
        }
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }
}
