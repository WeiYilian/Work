using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mage : EnemyConcroller
{
    [Header("Mage")]
    public GameObject attackPoint;

    private int warriorNumber = 0;
    
    public int maxWarriorNumber = 3;
    
    public float spawnWarriorOdd = 0.3f;

    protected override void Attack()
    {
        if (TargetInAttackRange())
        {
            if(Random.value <= spawnWarriorOdd && warriorNumber <= maxWarriorNumber)
                animator.SetTrigger("Attack");//召唤骷髅
            else
                animator.SetTrigger("Skill");//法球攻击
        }
        
    }

    public override void Hit()
    {
        GameObject mageSkill = ObjectPool.Instance.Get("MageSkill");
        mageSkill.transform.position = attackPoint.transform.position;
        mageSkill.GetComponent<MageAttack>().attackTarget = AttackTarget;
    }

    public override void KickOff()
    {
        GameObject mageAttack = ObjectPool.Instance.Get("MageAttack",transform.parent);
        AudioManager.Instance.PlayAudio(4,"Summon");
        Vector3 spawnPo = RandomSpawnPoint();
        mageAttack.transform.position = spawnPo;

        GameObject warrior = ObjectPool.Instance.Get("Warrior",transform.parent);
        warrior.transform.position = spawnPo;
        StartCoroutine(SpawnWarrior(warrior,mageAttack));
        warriorNumber++;
    }

    
    /// <summary>
    /// 随机刷新点
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomSpawnPoint()
    {
        Vector3 randomSpawnPoint = Vector3.zero;
        while (randomSpawnPoint == Vector3.zero)
        {
            float randomX = Random.Range(-patrolRange, patrolRange);
            float randomZ = Random.Range(-patrolRange, patrolRange);
    
            Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
            
            NavMeshHit hit;
            randomSpawnPoint =  NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : Vector3.zero;
        }

        return randomSpawnPoint;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator SpawnWarrior(GameObject warrior,GameObject mageAttack)
    {
        warrior.GetComponent<EnemyConcroller>().isSpawn = true;
        yield return new WaitForSeconds(3);
        ObjectPool.Instance.Remove("MageAttack",mageAttack);
        warrior.GetComponent<EnemyConcroller>().isSpawn = false;
        warrior.GetComponent<Collider>().enabled = true;
    }
}
