using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController: EnemyController
{
  override protected void Awake(){
      index = (int)EnemyIndexes.CROW;
      rand = Random.Range(-6f, 6f);
      velocity = Random.Range(2f, 3f);
      currentHealth = 20;
      points = 10;
      dmg = 10;
      healDropChance = 25;
      powerUpDropChance = 25;
  }

  override protected void moveUpdate(){
      transform.rotation*= Quaternion.AngleAxis(Time.deltaTime, Vector3.back);
      Vector3 mov = new Vector3(-velocity-Mathf.Cos(Time.fixedTime+rand), Mathf.Sin(Time.fixedTime+rand), 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      velocity+=Time.deltaTime*0.1f;
  }
}
