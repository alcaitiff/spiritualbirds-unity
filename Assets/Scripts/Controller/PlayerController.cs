using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour,Hitable
{

  public int currentHealth = 3;
  public List<FBController> bullets = new List<FBController>();
  public List<FBController> orbitalBullets = new List<FBController>();
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
  private ShieldController shield;  
  [SerializeField]
  private FBController bulletPrefab;  
  [SerializeField]
  private HelperController helperPrefab;
  private bool pierce = false;
  private bool pulse = false;
  private bool bounce = false;
  private bool orbital = false;
  private float orbitalTime = 0f;
  private bool auto = false;
  private float pulseTime = 0f;
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
    if(pulse){
      pulseShoot(Time.deltaTime);
    }
    if(orbital){
      orbitalShoot(Time.deltaTime);
    }
    if(auto){
      StartShoot();
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

  public void pulseShoot(float deltaTime){
    if(pulse){
      pulseTime+=deltaTime;
      if(pulseTime>=shootRate*5){
        FBController bullet = instantiateBullet(0,getDMG(),0.4f,0f,0f);
        bullet.playShoot();
        ShootStar(22);
        pulseTime=0f;
      }
    }
  }

  public void orbitalShoot(float deltaTime){
    if(orbital){
      orbitalTime+=deltaTime;
      if(orbitalTime>=shootRate*6){
        if(orbitalBullets.Count<stats[(int)PowerIndexes.SPEED]){
          FBController bullet = Instantiate(bulletPrefab, transform);
          bullet.setOrbital().setAngle(-90).setDMG(getDMG()).setPierce(pierce).setPlayer(this).transform.localScale*=0.8f;
          bullet.playShoot();
          orbitalBullets.Add(bullet);
        }
        orbitalTime=0f;
      }
    }
  }

  public IEnumerator Shoot(bool repeat=false){
    if(bullets.Count<stats[(int)PowerIndexes.AMMO]){
      foreach (HelperController helper in helpers){
       helper.Shoot();
      }
      FBController bullet = instantiateBullet(0,getDMG(),1f,0f,0f);
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
    if(repeat && shooting){
      if(auto){
        yield return new WaitForSeconds(shootRate*0.3f+shootRate*0.7f/(float)stats[(int)PowerIndexes.AMMO]); 
      }else{
        yield return new WaitForSeconds(shootRate); 
      }
      shootCoroutine = StartCoroutine(Shoot(true));
    }else{
      yield break;
    }

  }

  public void ShootExtra(int num){
    int dmg = getHalfDMG();
    if(num>0){instantiateBullet(0f,   dmg,0.5f,0f,0.4f);}
    if(num>1){instantiateBullet(0f,   dmg,0.5f,0f, -0.4f);}
    if(num>2){instantiateBullet(15f,  dmg,0.4f,0f, 0.6f);}
    if(num>3){instantiateBullet(-15f, dmg,0.4f,0f, -0.6f);}
    if(num>4){instantiateBullet(30f,  dmg,0.4f,0f, 0.6f);}
    if(num>5){instantiateBullet(-30f, dmg,0.4f,0f, -0.6f);}
    if(num>6){instantiateBullet(45f,  dmg,0.4f,0f, 0.6f);}
    if(num>7){instantiateBullet(-45f, dmg,0.4f,0f, -0.6f);}
    if(num>8){instantiateBullet(60f,  dmg,0.4f,0f, 0.6f);}
    if(num>9){instantiateBullet(-60f, dmg,0.4f,0f, -0.6f);}
  }
  public void ShootStar(int num){
      Vector3 offset = new Vector3(1f, 0, 0) + transform.position;
      int dmg = getDMG();
      if(num>0){ instantiateBullet(15f,   dmg,0.4f,0f,   0.4f);}
      if(num>1){ instantiateBullet( -15f, dmg,0.4f,0f,  -0.4f);}
      if(num>2){ instantiateBullet( 30f,  dmg,0.4f,0f,   0.6f);}
      if(num>3){ instantiateBullet( -30f, dmg,0.4f,0f,  -0.6f);}
      if(num>4){ instantiateBullet( 45f,  dmg,0.4f,0f,   0.6f);}
      if(num>5){ instantiateBullet( -45f, dmg,0.4f,0f,  -0.6f);}
      if(num>6){ instantiateBullet( 60f,  dmg,0.4f,0f,   0.6f);}
      if(num>7){ instantiateBullet( -60f, dmg,0.4f,0f,  -0.6f);}
      if(num>8){ instantiateBullet( 75f,  dmg,0.4f,0f,   0.6f);}
      if(num>9){ instantiateBullet( -75f, dmg,0.4f,0f,  -0.6f);}
      if(num>10){instantiateBullet( 90f,  dmg,0.4f,0f,   0.4f);}
      if(num>11){instantiateBullet( -90f, dmg,0.4f,0f,  -0.4f);}
      if(num>12){instantiateBullet( 105f, dmg,0.4f,0f,   0.6f);}
      if(num>13){instantiateBullet( -105f,dmg,0.4f,0f,  -0.6f);}
      if(num>14){instantiateBullet( 120f, dmg,0.4f,0f,   0.6f);}
      if(num>15){instantiateBullet( -120f,dmg,0.4f,0f,  -0.6f);}
      if(num>16){instantiateBullet( 135f, dmg,0.4f,0f,   0.6f);}
      if(num>17){instantiateBullet( -135f,dmg,0.4f,0f,  -0.6f);}
      if(num>18){instantiateBullet( 150f, dmg,0.4f,0f,   0.6f);}
      if(num>19){instantiateBullet( -150f,dmg,0.4f,0f,  -0.6f);}
      if(num>20){instantiateBullet( 165f, dmg,0.4f,0f,   0.4f);}
      if(num>21){instantiateBullet( -165f,dmg,0.4f,0f,  -0.4f);}
      if(num>22){instantiateBullet( 180f, dmg,0.4f,0f,   0f);}
      if(num>22){instantiateBullet( 0f,   dmg,0.4f,2f,   0f);}
  }

  private FBController instantiateBullet(float angle,int dmg,float scale,float x,float y){
      Vector3 offset = new Vector3(1f, 0, 0) + transform.position;
      FBController bullet = Instantiate(bulletPrefab, offset+new Vector3(x, y, 0), Quaternion.identity );
      bullet.setDMG(dmg).setBounce(bounce).setPierce(pierce).setPlayer(this).setAngle(angle).transform.localScale*=scale;
      return bullet;
  }

  public void removeBullet(FBController bullet){
      if(!bullets.Remove(bullet)){
        orbitalBullets.Remove(bullet);
      }
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
    AudioSource.PlayClipAtPoint(cry, new Vector3(0f,0f,-10f));
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
        auto = true;
        break;
      case (int)SuperIndexes.BOUNCE:
        bounce = true;
        break;
      case (int)SuperIndexes.ORBITAL:
        orbital = true;
        break;
      case (int)SuperIndexes.PIERCE:
        pierce = true;
        break;
      case (int)SuperIndexes.PULSE:
        pulse = true;
        break;
      case (int)SuperIndexes.REGEN:
        regenActive = true;
        break;
      case (int)SuperIndexes.SHIELD:
        shield.gameObject.SetActive(true);
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
      AudioSource.PlayClipAtPoint(death, new Vector3(0f,0f,-10f));
    }
    if(regenActive && regenCoroutine==null && currentHealth<getMaxHP()){
      regenCoroutine = StartCoroutine(regen());
    } 
    gm.setLife(currentHealth,getMaxHP());
  }
}
