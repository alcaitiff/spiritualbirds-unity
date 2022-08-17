using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBController : MonoBehaviour
{
  public int dmg = 1;
  private float velocity = 8f;
  private PlayerController player;
  [SerializeField]
  public AudioClip audioHit;
  [SerializeField]
  public AudioClip audioShoot;
  void Start()
  {
    AudioSource.PlayClipAtPoint(audioShoot, transform.position);
  }

  public FBController setPlayer(PlayerController player){
    this.player = player;
    return this;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 mov = new Vector3(velocity, 0, 0);
    Vector3 des = transform.position + mov * Time.deltaTime;
    transform.position = des;
    if(des.x>12){
        Destroy(gameObject);
    }
  }
  void OnCollisionEnter2D(Collision2D other) {
    if(other.collider.tag=="Enemy"){
      AudioSource.PlayClipAtPoint(audioHit, transform.position);
      EnemyController e = other.gameObject.GetComponent<EnemyController>();
      int p = e.hit(dmg);
      player.addPoints(p);
      Destroy(gameObject);
    };
  }
  private void OnDestroy() {
    if(player){
      player.removeBullet(this);
    }
  }
}
