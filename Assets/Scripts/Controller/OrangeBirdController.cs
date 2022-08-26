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
      spread = 1;
      healDropChance = 25;
      powerUpDropChance = 75;
  }

  override protected void moveUpdate(){
      transform.rotation*= Quaternion.AngleAxis(Time.deltaTime, Vector3.back);
      Vector3 mov = new Vector3(-velocity-Mathf.Cos(Time.fixedTime+rand)/2f, Mathf.Sin(Time.fixedTime+rand)*1.5f, 0);
      Vector3 des = transform.position + mov * Time.deltaTime;
      transform.position = des;
  }
  override protected void Shoot(){
      Vector3 offset = new Vector3(-0.5f, -0.2f, 0f);
      Vector3 pos = transform.position + offset;
      GBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
      bullet.setDMG(dmg);
      bullet.playShoot();
      ShootExtra(spread-1);
  }
  override protected void ShootExtra(int num){
      Vector3 offset = new Vector3(-0.5f, -0.2f, 0f) + transform.position;
      if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(dmg).setAngle(40f);}
      if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(dmg).setAngle(-40f);}
      if(num>2){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(10f);}
      if(num>3){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-10f);}      
      if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(20f);}
      if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-20f);}      
      if(num>6){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(30f);}
      if(num>7){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-30f);}
      if(num>8){Instantiate(bulletPrefab, offset+new Vector3(1f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(15f);}
      if(num>9){Instantiate(bulletPrefab, offset+new Vector3(1f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-15f);}
  }
}
