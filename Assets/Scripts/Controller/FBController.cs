using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBController : MonoBehaviour
{
  public int dmg = 1;
  private float velocity = 8f;
  private bool pierce = false;
  private bool bounce = false;
  private bool orbital = false;
  private float angle = 0f;
  private PlayerController player;
  [SerializeField]
  public AudioClip audioHit;
  [SerializeField]
  public AudioClip audioShoot;
  public void playShoot()
  {
    AudioSource.PlayClipAtPoint(audioShoot, new Vector3(0f,0f,-10f),0.8f);
  }

  public FBController setPlayer(PlayerController player){
    this.player = player;
    return this;
  }
  
  public FBController setAngle(float degrees){
    this.angle = Mathf.PI*degrees/180;
    return this;
  }    
  
  public FBController setOrbital(bool value=true){
    this.orbital = value;
    return this;
  }    
  

  public float getAngleDegress(){
    return this.angle*180/Mathf.PI;
  }  
  
  public FBController setPierce(bool value){
    this.pierce = value;
    return this;
  }    
  
  public FBController setBounce(bool value){
    this.bounce = value;
    return this;
  }  
  
  public FBController setDMG(int dmg){
    this.dmg = dmg;
    return this;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 mov,des;
    if(orbital){
      transform.position = 1.8f * Vector3.Normalize(transform.position - player.transform.position ) + player.transform.position;  
      transform.RotateAround(player.transform.position, Vector3.forward, 215 * Time.deltaTime);

    }else{
      mov = new Vector3(velocity*Mathf.Cos(angle), velocity*Mathf.Sin(angle), 0);
      des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      transform.rotation= Quaternion.AngleAxis(getAngleDegress(), Vector3.forward);
      if(des.x>13 || des.x<-13 || des.y>5 || des.y<-5){
          Destroy(gameObject);
      }
    }
  }
  void OnTriggerEnter2D(Collider2D other){
    if(other.tag=="Enemy"){
      Hitable e = other.gameObject.GetComponent<Hitable>();
      if(!e.isDead()){
        AudioSource.PlayClipAtPoint(audioHit, new Vector3(0f,0f,-10f),0.4f);
        int p = e.hit(dmg);
        player.addPoints(p);
        if(bounce && !orbital){
          if(transform.position.x>=0 && transform.position.y>=0) {setAngle(-135);}
          if(transform.position.x<0 && transform.position.y>=0)  {setAngle(-45); }
          if(transform.position.x<0 && transform.position.y<0)   {setAngle(45);  } 
          if(transform.position.x>=0 && transform.position.y<0)  {setAngle(135); }
        }else if(!pierce){
          Destroy(gameObject);
        }
      }
    };
  }
  private void OnDestroy() {
    if(player){
      player.removeBullet(this);
    }
  }
}
