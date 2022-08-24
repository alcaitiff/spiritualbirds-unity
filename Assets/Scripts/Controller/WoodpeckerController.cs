using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodpeckerController: EnemyController
{
  override protected void Awake(){
      index = (int)EnemyIndexes.WOODPECKER;
      rand = Random.Range(-5f, 5f);
      velocity = Random.Range(1.5f, 2.5f);
      currentHealth = 2;
      points = 4;
      dmg = 2;
      healDropChance = 15;
      powerUpDropChance = 30;
  }

  override protected void moveUpdate(){
      Vector3 mov = new Vector3(-velocity, 0, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
  }

}
