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
  protected int powerUpDropChance = 60;
  protected bool dead = false;
  protected GameManager gm;
  protected int index;
  protected int spread = 1;
  [SerializeField]
  protected GBController bulletPrefab;
  [SerializeField]
  public AudioClip audioHit;

  // *********************************
  //              Protected
  // *********************************
  protected virtual void Awake() {
    index = (int)EnemyIndexes.PIDGEON;
    rand = Random.Range(-5f, 5f);
    velocity = Random.Range(1.3f, 1.8f); 
  }
   
  protected virtual void Start(){
    gm = GameManager.instance;
    gm.stats[index].born++;
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

  virtual protected void Shoot(){
    Vector3 offset = new Vector3(-2f, 0, 0);
    Vector3 pos = transform.position + offset;
    GBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
    bullet.setDMG(dmg);
    bullet.playShoot();
    ShootExtra(spread);
  }
  virtual protected void ShootExtra(int num){
    Vector3 offset = new Vector3(-1.5f, 0, 0) + transform.position;
    if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(dmg);}
    if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(dmg);}
    if(num>2){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(10f);}
    if(num>3){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-10f);}      
    if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(20f);}
    if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-20f);}      
    if(num>6){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(30f);}
    if(num>7){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-30f);}
    if(num>8){Instantiate(bulletPrefab, offset+new Vector3(1f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(15f);}
    if(num>9){Instantiate(bulletPrefab, offset+new Vector3(1f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-15f);}
  }
  // *********************************
  //              Public
  // *********************************
  public int hit(int dmg){
    AudioSource.PlayClipAtPoint(audioHit, transform.position);
    changeLife(-dmg);
    return dead?points:0;
  }
  public bool isDead(){
    return this.dead;
  }
  // *********************************
  //              Private
  // *********************************
  private void OnCollisionEnter2D(Collision2D other) {
    if(other.collider.tag=="Player"){
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      other.gameObject.GetComponent<PlayerController>().hit(dmg);
    };
  }
  private void death(){
    if(!dead){
      dead = true;
      calcDrops(transform.position);
      gameObject.GetComponent<Animator>().SetBool("Dead", true);
      gameObject.GetComponent<Rigidbody2D>().Sleep();
      gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
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
