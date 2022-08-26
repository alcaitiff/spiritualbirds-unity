using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodpeckerController: EnemyController
{
  override protected void Awake(){
      index = (int)EnemyIndexes.WOODPECKER;
      rand = Random.Range(-5f, 5f);
      velocity = Random.Range(1.5f, 2.5f);
      currentHealth = 3;
      points = 4;
      dmg = 2;
      spread = 2;
      healDropChance = 35;
      powerUpDropChance = 30;
  }

  override protected void moveUpdate(){
      Vector3 mov = new Vector3(-velocity, 0, 0);
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
