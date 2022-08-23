using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController :  MonoBehaviour,Hitable
{
  protected int currentHealth = 1;
  protected int points = 1;
  protected int dmg = 1;
  protected float velocity;
  protected float rand;
  protected int healDropChance = 10;
  protected int powerUpDropChance = 50;
  protected bool dead = false;
  protected GameManager gm;
  protected int index;
  [SerializeField]
  public AudioClip audioHit;
  // Start is called before the first frame update
  void Start(){
      index = (int)EnemyIndexes.PIDGEON;
      rand = Random.Range(-5f, 5f);
      velocity = Random.Range(1.3f, 1.8f);
      gm = GameManager.instance;
  }

  protected void deadUpdate(){
      Vector3 mov = new Vector3(+2, -7, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      transform.rotation= transform.rotation*Quaternion.AngleAxis(240*Time.deltaTime, Vector3.back);
  }
  protected void outOfBoundsUpdate(){
    if(transform.position.x<-12 || transform.position.y<-4.5){
          Destroy(gameObject);
    }
  }
  virtual protected void moveUpdate(){
    Vector3 mov = new Vector3(-velocity, Mathf.Sin(Time.fixedTime+rand)*0.7f, 0);
    Vector3 des = transform.position + mov * Time.deltaTime;
    transform.position = des;
  }
  // Update is called once per frame
  protected void Update(){
    if(dead){
      deadUpdate();
    }else{
      moveUpdate();
    }
    outOfBoundsUpdate();
  }

  void OnCollisionEnter2D(Collision2D other) {
    if(other.collider.tag=="Player"){
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      other.gameObject.GetComponent<PlayerController>().hit(dmg);
    };
  }

  public bool isDead(){
    return this.dead;
  }

  void death(){
    if(!dead){
      dead = true;
      calcDrops(transform.position);
      gameObject.GetComponent<Animator>().SetBool("Dead", true);
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

  private void OnDestroy() {
    if(dead){
      gm.stats[index].killed++;
    }else{
      gm.stats[index].slipped++;
    }
  }
}
