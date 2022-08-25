using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : EnemyController{

    override protected void Awake(){
        index = (int)EnemyIndexes.BOSS2;
        rand = 0f;
        velocity = 0f;
        currentHealth = 1;//000;
        points = 1000;
        dmg = 2;
        healDropChance = 0;
        powerUpDropChance = 0;
        spread = 10;
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
        yield return new WaitForSeconds(Random.Range(0.1f,4f));
        Shoot();
        StartCoroutine(attack());
    }

}
