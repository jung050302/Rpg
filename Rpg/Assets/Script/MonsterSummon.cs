using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummon : MonoBehaviour
{
    public GameObject[] monster;
    public int monsterCount;
    float timer = 0;
    void Start()
    {
        monsterCount = 0;
    }

    
    void Update()
    {
        if (monsterCount < 5)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                for (int i = 0; i < 5 - monsterCount; i++)
                {

                    GameObject mon = Instantiate(monster[Random.Range(0, monster.Length - 1)]);
                    mon.transform.position = new Vector2(Random.Range(-14.2f, 13.5f), Random.Range(93.6f, 79.5f));
                    monsterCount++;
                }
                timer = 0;
            }
             
        }
    }
}
