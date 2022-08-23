using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBController : MonoBehaviour
{
  public int dmg = 1;
  private float velocity = 8f;
  private float angle = 0f;
  private PlayerController player;
  [SerializeField]
  public AudioClip audioHit;
  [SerializeField]
  public AudioClip audioShoot;
  public void playShoot()
  {
    AudioSource.PlayClipAtPoint(audioShoot, transform.position);
  }

  public FBController setPlayer(PlayerController player){
    this.player = player;
    return this;
  }
  
  public FBController setAngle(float degrees){
    this.angle = Mathf.PI*degrees/180;
    return this;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 mov = new Vector3(velocity*Mathf.Cos(angle), velocity*Mathf.Sin(angle), 0);
    Vector3 des = transform.position + mov * Time.deltaTime;
    transform.position = des;
    if(des.x>12 || des.y>4.5 || des.y<-4.5){
        Destroy(gameObject);
    }
  }
  void OnTriggerEnter2D(Collider2D other){
    if(other.tag=="Enemy"){
      Hitable e = other.gameObject.GetComponent<Hitable>();
      if(!e.isDead()){
        AudioSource.PlayClipAtPoint(audioHit, transform.position);
        int p = e.hit(dmg);
        player.addPoints(p);
        Destroy(gameObject);
      }
    };
  }
  private void OnDestroy() {
    if(player){
      player.removeBullet(this);
    }
  }
}
