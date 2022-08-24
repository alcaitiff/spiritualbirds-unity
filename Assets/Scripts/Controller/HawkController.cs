using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkController: EnemyController
{
  private bool diving = false;

  override protected void Awake(){
      index = (int)EnemyIndexes.HAWK;
      rand = Random.Range(-5f, 5f);
      velocity = Random.Range(2f, 3f);
      currentHealth = 10;
      points = 6;
      dmg = 5;
      healDropChance = 15;
      powerUpDropChance = 15;
  }
  protected override void Start(){
      base.Start();
      transform.rotation= transform.rotation*Quaternion.AngleAxis(45, Vector3.back);
  }

  override protected void moveUpdate(){
    if(!diving && transform.position.y >4){
      diving=true;
      gameObject.GetComponent<Animator>().SetBool("Diving", true);
      transform.rotation= transform.rotation*Quaternion.AngleAxis(70, Vector3.forward);
    } else if(!diving){
      Vector3 mov = new Vector3(-velocity, +velocity, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
    }else{
      Vector3 mov = new Vector3(-velocity*3f, -velocity, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
    }
  }

}
