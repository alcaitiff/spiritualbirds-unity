using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueJayController: EnemyController
{
  // Start is called before the first frame update
  override protected void Awake(){
      index = (int)EnemyIndexes.BLUEJAY;
      rand = Random.Range(-6f, 6f);
      velocity = Random.Range(2f, 3f);
      currentHealth = 8;
      points = 6;
      dmg = 5;
      healDropChance = 25;
      powerUpDropChance = 25;
  }

  // Update is called once per frame
  override protected void moveUpdate(){
      transform.rotation*= Quaternion.AngleAxis(Time.deltaTime, Vector3.back);
      Vector3 mov = new Vector3(-velocity-Mathf.Cos(Time.fixedTime+rand), Mathf.Sin(Time.fixedTime+rand), 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
      velocity+=Time.deltaTime*0.3f;
  }

}
