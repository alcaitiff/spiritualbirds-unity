using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBController : MonoBehaviour
{
  public int dmg = 2;
  private float velocity = 5f;
  private float angle = 0f;
  [SerializeField]
  public AudioClip audioHit;
  [SerializeField]
  public AudioClip audioShoot;
  public void playShoot()
  {
    AudioSource.PlayClipAtPoint(audioShoot, transform.position);
  }

  public GBController setAngle(float degrees){
    this.angle = Mathf.PI*degrees/180;
    return this;
  }  
  
  public GBController setDMG(int dmg){
    this.dmg = dmg;
    return this;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 mov = new Vector3(-velocity*Mathf.Cos(angle), velocity*Mathf.Sin(angle), 0);
    Vector3 des = transform.position + mov * Time.deltaTime;
    transform.position = des;
    if(des.x<-12 || des.y>4.5 || des.y<-4.5){
        Destroy(gameObject);
    }
  }
  void OnTriggerEnter2D(Collider2D other){
    if(other.tag=="Player"){
      Hitable e = other.gameObject.GetComponent<Hitable>();
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      e.hit(dmg);
      Destroy(gameObject);
    };
  }
}
