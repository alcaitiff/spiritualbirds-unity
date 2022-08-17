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
  void Start()
  {
    maxStats[(int)PowerIndexes.SPEED] = 10;
    maxStats[(int)PowerIndexes.AMMO] = 10;
    maxStats[(int)PowerIndexes.DMG] = 100;
    maxStats[(int)PowerIndexes.HEALTH] = 100;
    maxStats[(int)PowerIndexes.SPREAD] = 10;
    maxStats[(int)PowerIndexes.OPTION] = 3;

    stats[(int)PowerIndexes.SPEED] = 0;
    stats[(int)PowerIndexes.AMMO] = 1;
    stats[(int)PowerIndexes.DMG] = 1;
    stats[(int)PowerIndexes.HEALTH] = 3;
    stats[(int)PowerIndexes.SPREAD] = 1;
    stats[(int)PowerIndexes.OPTION] = 0;
    gm = GameManager.instance;
    gm.player=this;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      AddPower(0);
    }
  }

  public void Shoot()
  {
    if(bullets.Count<stats[(int)PowerIndexes.AMMO]){
      Vector3 offset = new Vector3(0.5f, 0, 0);
      Vector3 pos = transform.position + offset;
      FBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
      bullet.setPlayer(this);
      bullets.Add(bullet);
    }
  }

  public void removeBullet(FBController bullet){
      bullets.Remove(bullet);
  }

  public void AddPower(int times)
  {
    int p = gm.powerInc();
    if (stats[p] >= maxStats[p] && times < 10)
    {
      AddPower(++times);
    }
  }

  public void PowerUp()
  {
    int p = gm.powerUp();
    if (p >-1 && stats[p] < maxStats[p])
    {
      stats[p]++;
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    //Debug.Log(other.collider.tag);
  }

  public void hit(int dmg){
    AudioSource.PlayClipAtPoint(cry, transform.position);
    currentHealth -= dmg;
    if(currentHealth<1){
      AudioSource.PlayClipAtPoint(death, transform.position);
    }
    gm.setLife(currentHealth,stats[(int)PowerIndexes.HEALTH]);
  }

  public void addPoints(int p){
    score+=p;
    gm.setScore(score);
  }
}
