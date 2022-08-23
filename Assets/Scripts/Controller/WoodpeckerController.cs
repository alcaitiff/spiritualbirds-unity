using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodpeckerController: EnemyController
{
  // Start is called before the first frame update
  void Start(){
      index = (int)EnemyIndexes.WOODPECKER;
      rand = Random.Range(-5f, 5f);
      velocity = Random.Range(1.5f, 2.5f);
      currentHealth = 2;
      points = 4;
      dmg = 2;
      healDropChance = 15;
      powerUpDropChance = 30;
      gm = GameManager.instance;
  }

  // Update is called once per frame
  override protected void Update(){
    if(dead){
      Vector3 mov = new Vector3(+2, -7, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      transform.rotation= transform.rotation*Quaternion.AngleAxis(240*Time.deltaTime, Vector3.back);
    }else{
      Vector3 mov = new Vector3(-velocity, 0, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
    }
    if(transform.position.x<-12 || transform.position.y<-4.5){
          Destroy(gameObject);
    }
  }

}
