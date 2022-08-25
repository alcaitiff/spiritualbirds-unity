using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonBossController : MonoBehaviour
{
    [SerializeField]
    private GBController bulletPrefab;
    private int spread = 10;
    private int dmg = 2;  
    public void Start(){
        StartCoroutine(kill());
    }
    void Update(){
        Vector3 mov = new Vector3(Mathf.Cos(Time.fixedTime)*0.7f, Mathf.Sin(Time.fixedTime)*0.7f, 0);
        Vector3 des = transform.position + mov * Time.deltaTime;
        transform.position = des; 
    }
    private IEnumerator kill(){
        yield return new WaitForSeconds(Random.Range(0.1f,3f));
        Shoot();
        StartCoroutine(kill());
    }
    public void Shoot(){
        Vector3 offset = new Vector3(-2f, 0, 0);
        Vector3 pos = transform.position + offset;
        GBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bullet.setDMG(dmg);
        bullet.playShoot();
        ShootExtra(spread);
    }
    public void ShootExtra(int num){
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
