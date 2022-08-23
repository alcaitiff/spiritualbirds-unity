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
    private float interval = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void enable(){
        active = true;
        StartCoroutine(spawnEnemy(interval, prefab));
    }

    public void disable(){
        this.active = false;
    }

    private IEnumerator spawnEnemy(float seconds, GameObject enemy)
    {
        yield return new WaitForSeconds(Random.Range(seconds,seconds*2));
        GameObject e = Instantiate(enemy, new Vector3(12f, Random.Range(-3.5f, 3f), 0), Quaternion.identity);
        if(this.active){
            StartCoroutine(spawnEnemy(seconds, enemy));
        }
    }

    public Spawner setInterval(float v){
        interval=v;
        return this;
    }
}
