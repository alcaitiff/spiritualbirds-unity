using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilJayController : EnemyController{

    override protected void Awake(){
        index = (int)EnemyIndexes.DEVILJAY;
        rand = 0f;
        velocity = 0f;
        currentHealth = 2000;
        points = 1000;
        dmg = 2;
        healDropChance = 0;
        powerUpDropChance = 0;
        spread = 12;
    }
    override protected void Start(){
        base.Start();
        StartCoroutine(attack());
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

    private IEnumerator attack(){
        yield return new WaitForSeconds(Random.Range(1f,4f));
        Shoot();
        StartCoroutine(attack());
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
        float r = Random.Range(-20f,20f);
        if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setDMG(dmg);}
        if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setDMG(dmg);}
        if(num>2){Instantiate(bulletPrefab, offset+new Vector3(-1f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+12f);}
        if(num>3){Instantiate(bulletPrefab, offset+new Vector3(-1f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+-12f);}      
        if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+22f);}
        if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+-22f);}      
        if(num>6){Instantiate(bulletPrefab, offset+new Vector3(-1f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+32f);}
        if(num>7){Instantiate(bulletPrefab, offset+new Vector3(-1f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+-32f);}
        if(num>8){Instantiate(bulletPrefab, offset+new Vector3(1f, 0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(r+17f);}
        if(num>9){Instantiate(bulletPrefab, offset+new Vector3(1f, -0.6f, 0), Quaternion.identity).setDMG(dmg).setAngle(-17f);}
        if(num>10){Instantiate(bulletPrefab, offset+new Vector3(1f, 0.6f, 0), Quaternion.identity).setDMG(dmg);}
        if(num>11){Instantiate(bulletPrefab, offset+new Vector3(1f, -0.6f, 0), Quaternion.identity).setDMG(dmg);}
    }

}
