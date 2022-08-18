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
  [SerializeField]
  public AudioClip audioHit;
  // Start is called before the first frame update
  void Start()
  {
      this.rand = Random.Range(-5f, 5f);
      this.velocity = Random.Range(1.3f, 1.8f);
  }

  // Update is called once per frame
  void Update()
  {
    if(!isDead()){
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
      PlayerController p = other.gameObject.GetComponent<PlayerController>();
      p.hit(dmg);
    };
  }

  void death(){
    Animator animator = gameObject.GetComponent<Animator>();
    animator.SetInteger("Dead", 1);
    Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
    body.gravityScale = 1;
    body.freezeRotation = false;
    body.velocity = new Vector2(4f, -3f);
    body.angularVelocity = -100f;
  }

  bool isDead(){
    return currentHealth<1;
  }

  public int hit(int dmg){
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      currentHealth-=dmg;
      if(isDead()){
          death();
          return points;
      }else{
          return 0;
      }
  }
}
