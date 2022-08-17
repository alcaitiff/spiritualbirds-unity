using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int currentHealth = 1;
    public int points = 1;
    public int dmg = 1;
    public float velocity;
    private float rand;
    [SerializeField]
    public AudioClip audioHit;
    // Start is called before the first frame update
    void Start()
    {
        this.rand = Random.Range(-5f, 5f);
        this.velocity = Random.Range(1.3f, 1.8f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = new Vector3(-velocity, Mathf.Sin(Time.fixedTime+rand)*0.7f, 0);
        Vector3 des = transform.position + mov * Time.deltaTime;
        transform.position = des;
        if(des.x<-12){
             Destroy(gameObject);
        }
    }

  void OnCollisionEnter2D(Collision2D other) {
    if(other.collider.tag=="Player"){
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      PlayerController p = other.gameObject.GetComponent<PlayerController>();
      p.hit(dmg);
    };
  }

    public int hit(int dmg){
        AudioSource.PlayClipAtPoint(audioHit, transform.position);
        currentHealth-=dmg;
        if(currentHealth<1){
            Destroy(gameObject);
            return points;
        }else{
            return 0;
        }
    }
}
