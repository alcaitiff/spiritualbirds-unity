using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBirdController: EnemyController
{
  override protected void Awake(){
      index = (int)EnemyIndexes.ORANGE_BIRD;
      rand = Random.Range(-4f, 4f);
      velocity = Random.Range(2f, 3f);
      currentHealth = 20;
      points = 6;
      dmg = 15;
      healDropChance = 25;
      powerUpDropChance = 75;
  }

  override protected void moveUpdate(){
      transform.rotation*= Quaternion.AngleAxis(Time.deltaTime, Vector3.back);
      Vector3 mov = new Vector3(-velocity-Mathf.Cos(Time.fixedTime+rand)/2f, Mathf.Sin(Time.fixedTime+rand)*1.5f, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
  }

}
