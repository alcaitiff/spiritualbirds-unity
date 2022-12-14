using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevileonController : EnemyController{

    override protected void Awake(){
        index = (int)EnemyIndexes.DEVILEON;
        rand = 0f;
        velocity = 0f;
        currentHealth = 1000;
        points = 1000;
        dmg = 2;
        healDropChance = 0;
        powerUpDropChance = 0;
        spread = 10;
    }
    override protected void Start(){
        base.Start();
        StartCoroutine(attack(0.1f,4f));
    }

    override protected void moveUpdate(){
        Vector3 mov = new Vector3(Mathf.Cos(Time.fixedTime)*0.7f, Mathf.Sin(Time.fixedTime)*0.7f, 0);
        Vector3 des = transform.position + mov * Time.deltaTime;
        transform.position = des; 
    }

    override protected void deadUpdate(){
        Vector3 mov = new Vector3(+1, -2, 0);
        Vector3 des = transform.position + mov * Time.deltaTime;
        transform.position = des;
        transform.rotation= transform.rotation*Quaternion.AngleAxis(90*Time.deltaTime, Vector3.back);
    }
    
    override protected void Shoot(){
        Vector3 offset = new Vector3(-2f, 0, 0);
        Vector3 pos = transform.position + offset;
        GBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bullet.setDMG(dmg);
        bullet.playShoot();
        ShootExtra(spread);
    }
  
    override protected void ShootExtra(int num){
        Vector3 offset = new Vector3(-1.5f, 0, 0) + transform.position;
        if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(dmg);}
        if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(dmg);}
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
