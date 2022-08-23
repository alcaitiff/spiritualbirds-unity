using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkController: EnemyController
{
  private bool diving = false;
  // Start is called before the first frame update
  void Start(){
      index = (int)EnemyIndexes.HAWK;
      rand = Random.Range(-5f, 5f);
      velocity = Random.Range(1.5f, 2.5f);
      currentHealth = 4;
      points = 6;
      dmg = 5;
      healDropChance = 15;
      powerUpDropChance = 15;
      gm = GameManager.instance;
      transform.rotation= transform.rotation*Quaternion.AngleAxis(45, Vector3.back);
  }

  // Update is called once per frame
  override protected void Update(){
    if(dead){
      Vector3 mov = new Vector3(+2, -7, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      transform.rotation= transform.rotation*Quaternion.AngleAxis(240*Time.deltaTime, Vector3.back);
    }else if(!diving && transform.position.y >3.5){
      diving=true;
      gameObject.GetComponent<Animator>().SetBool("Diving", true);
      transform.rotation= transform.rotation*Quaternion.AngleAxis(90, Vector3.forward);
    } else if(!diving){
      Vector3 mov = new Vector3(-velocity, +velocity, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
    }else{
      Vector3 mov = new Vector3(-velocity*1.5f, -velocity*1.5f, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
    }
    if(transform.position.x<-12 || transform.position.y<-4.5){
          Destroy(gameObject);
    }
  }

}
