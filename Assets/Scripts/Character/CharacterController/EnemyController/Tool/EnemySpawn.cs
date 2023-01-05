using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    //刷新范围
    public float Range = 4;

    public float mageOdd = 0.3f;

    private int mageNumber;
    
    private int warriorNumber;
    
    public int maxMageNumber = 1;
    
    public int maxWarriorNumber = 3; 
    //刷新间隔
    public float intervalTime = 10f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(RandomSpawnEnemy),1f,intervalTime);
    }

    public void RandomSpawnEnemy()
    {
        bool isMage = Random.value < mageOdd;
        if (isMage)
        {
            if (maxMageNumber <= mageNumber) return;
            GameObject go = ObjectPool.Instance.Get("Mage",transform);
            go.transform.position = RandomSpawnPoint();
            go.GetComponent<EnemyConcroller>().guardPos = go.transform.position;
            mageNumber++;
        }
        else
        {
            if (maxWarriorNumber <= warriorNumber) return;
            GameObject go = ObjectPool.Instance.Get("Warrior",transform);
            go.transform.position = RandomSpawnPoint();
            go.GetComponent<EnemyConcroller>().guardPos = go.transform.position;
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
