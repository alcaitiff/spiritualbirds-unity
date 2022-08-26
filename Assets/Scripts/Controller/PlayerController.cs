using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour,Hitable
{

  public int currentHealth = 3;
  public ArrayList bullets = new ArrayList();
  public List<HelperController> helpers = new List<HelperController>();
  public int score = 0;
  public AudioClip death;
  public AudioClip cry;
  private bool regenActive = false;
  private Coroutine regenCoroutine;
  public GameManager gm;
  private int[] maxStats = new int[6];
  private int[] stats = new int[6];
  
  [SerializeField]
  private FBController bulletPrefab;  
  [SerializeField]
  private HelperController helperPrefab;
  
  private bool shooting = false;
  private float shootRate = 0.5f;
  private Coroutine shootCoroutine;
  void Awake(){
    maxStats[(int)PowerIndexes.SPEED] = 10;
    maxStats[(int)PowerIndexes.AMMO] = 10;
    maxStats[(int)PowerIndexes.DMG] = 100;
    maxStats[(int)PowerIndexes.HEALTH] = 100;
    maxStats[(int)PowerIndexes.SPREAD] = 11;
    maxStats[(int)PowerIndexes.OPTION] = 3;

    stats[(int)PowerIndexes.SPEED] = 3;
    stats[(int)PowerIndexes.AMMO] = 1;
    stats[(int)PowerIndexes.DMG] = 1;
    stats[(int)PowerIndexes.HEALTH] = 3;
    stats[(int)PowerIndexes.SPREAD] = 1;
    stats[(int)PowerIndexes.OPTION] = 0;

  }
  void Start(){
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
  public int getDMG(){
    return stats[(int)PowerIndexes.DMG];
  }  
  public int getHalfDMG(){
    int dmg = this.getDMG();
    return dmg>2?dmg/2:1;
  }
  public void spawnHelper(){
    if(helpers.Count<stats[(int)PowerIndexes.OPTION]){
      Vector3 offset = new Vector3(-6f, -4f, 0f);
      Vector3 pos = transform.position + offset;
      HelperController helper = Instantiate(helperPrefab, pos, Quaternion.identity);
      helper.target = helpers.Count == 0? transform:helpers[helpers.Count -1].transform;
      helpers.Add(helper);
    }
  }
  
  public void StartShoot(){
    if(!shooting){
      shooting = true;
      shootCoroutine = StartCoroutine(Shoot(true));
    }
  }

  public void StopShoot(){
    shooting = false;
    if(shootCoroutine!=null){
      StopCoroutine(shootCoroutine);
    }
  }

  public IEnumerator Shoot(bool auto=false){
    if(bullets.Count<stats[(int)PowerIndexes.AMMO]){
      foreach (HelperController helper in helpers){
       helper.Shoot();
      }
      Vector3 offset = new Vector3(1f, 0, 0);
      Vector3 pos = transform.position + offset;
      FBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
      bullet.setDMG(getDMG()).setPlayer(this);
      bullets.Add(bullet);
      bullet.playShoot();
      if(getSpread()>1){
        if(maxStats[(int)PowerIndexes.SPREAD]>11){
          ShootStar(getSpread()-1);
        }else{
          ShootExtra(getSpread()-1);
        }
      }
    }    
    if(auto && shooting){
      yield return new WaitForSeconds(shootRate); 
      shootCoroutine = StartCoroutine(Shoot(true));
    }else{
      yield break;
    }

  }

  public void ShootExtra(int num){
    Vector3 offset = new Vector3(1f, 0, 0) + transform.position;
    if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).transform.localScale*=0.5f;}
    if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).transform.localScale*=0.5f;}
    if(num>2){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(15f).transform.localScale*=0.4f;}
    if(num>3){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(-15f).transform.localScale*=0.4f;}      
    if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(30f).transform.localScale*=0.4f;}
    if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(-30f).transform.localScale*=0.4f;}      
    if(num>6){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(45f).transform.localScale*=0.4f;}
    if(num>7){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(-45f).transform.localScale*=0.4f;}
    if(num>8){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(60f).transform.localScale*=0.4f;}
    if(num>9){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getHalfDMG()).setPlayer(this).setAngle(-60f).transform.localScale*=0.4f;}
  }
  public void ShootStar(int num){
      Vector3 offset = new Vector3(1f, 0, 0) + transform.position;
      if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(15f).transform.localScale*=0.4f;}
      if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-15f).transform.localScale*=0.4f;}
      if(num>2){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(30f).transform.localScale*=0.4f;}
      if(num>3){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-30f).transform.localScale*=0.4f;}
      if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(45f).transform.localScale*=0.4f;}
      if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-45f).transform.localScale*=0.4f;}
      if(num>6){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(60f).transform.localScale*=0.4f;}
      if(num>7){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-60f).transform.localScale*=0.4f;}
      if(num>8){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(75f).transform.localScale*=0.4f;}
      if(num>9){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-75f).transform.localScale*=0.4f;}      
      
      if(num>10){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(90f).transform.localScale*=0.4f;}
      if(num>11){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-90f).transform.localScale*=0.4f;}
      if(num>12){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(105f).transform.localScale*=0.4f;}
      if(num>13){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-105f).transform.localScale*=0.4f;}      
      if(num>14){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(120f).transform.localScale*=0.4f;}
      if(num>15){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-120f).transform.localScale*=0.4f;}      
      if(num>16){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(135f).transform.localScale*=0.4f;}
      if(num>17){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-135f).transform.localScale*=0.4f;}
      if(num>18){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(150f).transform.localScale*=0.4f;}
      if(num>19){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-150f).transform.localScale*=0.4f;}
      if(num>20){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(165f).transform.localScale*=0.4f;}
      if(num>21){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(-165f).transform.localScale*=0.4f;}
      if(num>22){Instantiate(bulletPrefab, offset+new Vector3(0f, 0f, 0), Quaternion.identity).setDMG(getDMG()).setPlayer(this).setAngle(180f).transform.localScale*=0.4f;}
      if(num>22){Instantiate(bulletPrefab, offset+new Vector3(2f, 0f, 0), Quaternion.identity).setDMG(getDMG()*2).setPlayer(this).transform.localScale*=2f;}      
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
      }else if((int)PowerIndexes.OPTION==p){
        spawnHelper();
      }
    }
  }

  public void Heal(){
    if (currentHealth < this.getMaxHP()) {
      changeCurrentHealth(1);
    }
  }

  public int hit(int dmg){
    AudioSource.PlayClipAtPoint(cry, transform.position);
    changeCurrentHealth(-dmg);
    return 0;
  }

  public bool isDead(){
    return currentHealth<1;
  }

  public void addPoints(int p){
    score+=p;
    gm.setScore(score);
  }

  public void addSuper(int index){
    switch (index)
    {
      case (int)SuperIndexes.AUTO:
      break;
      case (int)SuperIndexes.BOUNCE:
      break;
      case (int)SuperIndexes.ORBITAL:
      break;
      case (int)SuperIndexes.PIERCE:
      break;
      case (int)SuperIndexes.PULSE:
      break;
      case (int)SuperIndexes.REGEN:
      regenActive = true;
      break;
      case (int)SuperIndexes.SHIELD:
      break;
      case (int)SuperIndexes.STAR:
        maxStats[(int)PowerIndexes.SPREAD] = 24;
      break;
      default:
      break;
    }
  }

  private IEnumerator regen(){
    yield return new WaitForSeconds(5f);
    regenCoroutine = null;
    Heal();
  }

  private void changeCurrentHealth(int value){
    currentHealth += value;
    if(currentHealth>getMaxHP()){
      currentHealth=getMaxHP();
    };
    if(currentHealth<1){
      AudioSource.PlayClipAtPoint(death, transform.position);
    }
    if(regenActive && regenCoroutine==null && currentHealth<getMaxHP()){
      regenCoroutine = StartCoroutine(regen());
    } 
    gm.setLife(currentHealth,getMaxHP());
  }
}
