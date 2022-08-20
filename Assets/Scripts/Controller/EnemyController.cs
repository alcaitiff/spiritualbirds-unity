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
  private int healDropChance = 10;
  private int powerUpDropChance = 50;
  public bool dead = false;
  private GameManager gm;
  [SerializeField]
  public AudioClip audioHit;
  // Start is called before the first frame update
  void Start(){
      this.rand = Random.Range(-5f, 5f);
      this.velocity = Random.Range(1.3f, 1.8f);
      gm = GameManager.instance;
  }

  // Update is called once per frame
  void Update(){
    if(dead){
      Vector3 mov = new Vector3(+2, -7, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      transform.rotation= transform.rotation*Quaternion.AngleAxis(240*Time.deltaTime, Vector3.back);
    }else{
      Vector3 mov = new Vector3(-velocity, Mathf.Sin(Time.fixedTime+rand)*0.7f, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
    }
    if(transform.position.x<-12 || transform.position.y<-4.5){
          Destroy(gameObject);
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    if(other.collider.tag=="Player"){
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      other.gameObject.GetComponent<PlayerController>().hit(dmg);
    };
  }

  void death(){
    if(!dead){
      dead = true;
      calcDrops(transform.position);
      gameObject.GetComponent<Animator>().SetInteger("Dead", 1);
      gameObject.GetComponent<Rigidbody2D>().Sleep();
      gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
  }

  public int hit(int dmg){
    AudioSource.PlayClipAtPoint(audioHit, transform.position);
    changeLife(-dmg);
    return dead?points:0;
  }

  private void calcDrops(Vector3 position){
    if(drop(powerUpDropChance)){
      gm.spawnPowerUp(position);
    }else if(drop(healDropChance)){
      gm.spawnHeal(position);
    }
  }

  private bool drop(int chance){
    return Random.Range(0,100)<=chance;
  }

  private void changeLife(int value){
    currentHealth+=value;
    if(currentHealth<1){
      death();
    }
  }
}
