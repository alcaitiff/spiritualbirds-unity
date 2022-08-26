using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    public bool active = false;
    [SerializeField]
    public bool attack;
    public float minRate = 1f;
    public float maxRate = 10f;

    [SerializeField]
    private float interval = 1f;

    public void enable(){
        active = true;
        StartCoroutine(spawnEnemy(interval, prefab));
    }

    public void disable(){
        this.active = false;
    }

    private IEnumerator spawnEnemy(float seconds, GameObject enemy)
    {
        yield return new WaitForSeconds(Random.Range(seconds,seconds*1.5f));
        GameObject e = Instantiate(enemy, new Vector3(12f, Random.Range(-3.5f, 3f), 0), Quaternion.identity);
        if(attack){
            StartCoroutine(e.GetComponent<EnemyController>().attack(minRate,maxRate));
        }
        if(this.active){
            StartCoroutine(spawnEnemy(seconds, enemy));
        }
    }

    public Spawner setInterval(float v){
        interval=v;
        return this;
    }
}
