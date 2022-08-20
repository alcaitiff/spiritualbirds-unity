using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

  public int currentHealth = 3;
  public ArrayList bullets = new ArrayList();
  public int score = 0;
  public AudioClip death;
  public AudioClip cry;
  public GameManager gm;
  private int[] maxStats = new int[6];
  private int[] stats = new int[6];
  
  [SerializeField]
  private FBController bulletPrefab;
  // Start is called before the first frame update
  void Start(){
    maxStats[(int)PowerIndexes.SPEED] = 10;
    maxStats[(int)PowerIndexes.AMMO] = 10;
    maxStats[(int)PowerIndexes.DMG] = 100;
    maxStats[(int)PowerIndexes.HEALTH] = 100;
    maxStats[(int)PowerIndexes.SPREAD] = 11;
    maxStats[(int)PowerIndexes.OPTION] = 3;

    stats[(int)PowerIndexes.SPEED] = 1;
    stats[(int)PowerIndexes.AMMO] = 1;
    stats[(int)PowerIndexes.DMG] = 1;
    stats[(int)PowerIndexes.HEALTH] = 3;
    stats[(int)PowerIndexes.SPREAD] = 1;
    stats[(int)PowerIndexes.OPTION] = 0;
    gm = GameManager.instance;
    gm.player=this;
  }

  //Update is called once per frame
  void Update(){
    if (Input.GetKeyDown(KeyCode.E))
    {
      AddPower(0);
    }
  }

  public int getSpeed(){
    return stats[(int)PowerIndexes.SPEED];
  }
  public int getMaxHP(){
    return stats[(int)PowerIndexes.HEALTH];
  }
  public int getMaxBullets(){
    return stats[(int)PowerIndexes.AMMO];
  }
  public int getSpread(){
    return stats[(int)PowerIndexes.SPREAD];
  }
  public void Shoot(){
    if(bullets.Count<stats[(int)PowerIndexes.AMMO]){
      Vector3 offset = new Vector3(1f, 0, 0);
      Vector3 pos = transform.position + offset;
      FBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
      bullet.setPlayer(this);
      bullets.Add(bullet);
      if(getSpread()>1){
        ShootExtra(getSpread()-1);
      }
    }
  }
  
  public void ShootExtra(int num){
      Vector3 offset = new Vector3(1f, 0, 0) + transform.position;
      if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setPlayer(this).transform.localScale*=0.5f;}
      if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setPlayer(this).transform.localScale*=0.5f;}
      if(num>2){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(15f).transform.localScale*=0.4f;}
      if(num>3){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(-15f).transform.localScale*=0.4f;}      
      if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(30f).transform.localScale*=0.4f;}
      if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(-30f).transform.localScale*=0.4f;}      
      if(num>6){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(45f).transform.localScale*=0.4f;}
      if(num>7){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(-45f).transform.localScale*=0.4f;}
      if(num>8){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(60f).transform.localScale*=0.4f;}
      if(num>9){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(this).setAngle(-60f).transform.localScale*=0.4f;}
  }
  public void removeBullet(FBController bullet){
      bullets.Remove(bullet);
  }

  public void AddPower(int times = 0){
    int p = gm.powerInc();
    if (stats[p] >= maxStats[p] && times < 10)
    {
      AddPower(++times);
    }
  }

  public void PowerUp(){
    int p = gm.powerUp();
    if (p >-1 && stats[p] < maxStats[p])
    {
      stats[p]++;
      if((int)PowerIndexes.HEALTH==p){
        changeCurrentHealth(0);
      }
    }
  }

  public void Heal(){
    if (currentHealth < this.getMaxHP()) {
      changeCurrentHealth(1);
    }
  }

  public void hit(int dmg){
    AudioSource.PlayClipAtPoint(cry, transform.position);
    changeCurrentHealth(-dmg);
  }

  public void addPoints(int p){
    score+=p;
    gm.setScore(score);
  }

  private void changeCurrentHealth(int value){
    currentHealth += value;
    if(currentHealth<1){
      AudioSource.PlayClipAtPoint(death, transform.position);
    }
    gm.setLife(currentHealth,getMaxHP());
  }
}
