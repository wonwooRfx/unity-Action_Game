using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwn : MonoBehaviour
{
    public GameObject enemy;
    public float intervalTime = 10f;
    public Transform[] spawnPools;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", intervalTime, intervalTime);
    }
    void Spawn()
    {
        // 몬스터 생성 지점 중에 하나를 랜덤으로 선택
        int spawnPoolIndex = Random.Range(0, spawnPools.Length);
        // 새로운 슬라임 생성
        Instantiate(enemy, spawnPools[spawnPoolIndex].position, spawnPools[spawnPoolIndex].rotation);

       /* if (spawnPools.Length > 0)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if(monsterCount < intervalTime)
            {
                int idx = Random.Range(0, spawnPools.Length);
                Instantiate(enemy, spawnPools[idx].position, spawnPools[idx].rotation);
            }
             
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
