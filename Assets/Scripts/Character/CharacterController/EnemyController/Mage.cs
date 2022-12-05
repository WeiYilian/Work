using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mage : EnemyConcroller
{
    [Header("Mage")]
    public GameObject attackPoint;

    public override void Hit()
    {
        GameObject MageSkill = GameFacade.Instance.LoadSlash("MageSkill");
        MageSkill.transform.position = attackPoint.transform.position;
        MageSkill.GetComponent<MageAttack>().attackTarget = AttackTarget;
    }

    public override void KickOff()
    {
        GameObject MageAttack = GameFacade.Instance.LoadSlash("MageAttack");
        MageAttack.transform.position = RandomSpawnPoint();
        GameObject Warrior = GameFacade.Instance.LoadEnemy("Warrior");
        Warrior.transform.position = MageAttack.transform.position;
        Warrior.GetComponent<EnemyConcroller>().isSpawn = true;
        StartCoroutine(SpawnWarrior(Warrior,MageAttack));
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
    IEnumerator SpawnWarrior(GameObject Warrior,GameObject MageAttack)
    {
        yield return new WaitForSeconds(10);
        Destroy(MageAttack);
        Warrior.GetComponent<EnemyConcroller>().isSpawn = false;
        Warrior.GetComponent<Collider>().enabled = true;
    }
}
