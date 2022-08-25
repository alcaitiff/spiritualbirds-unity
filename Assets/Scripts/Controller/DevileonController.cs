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
        StartCoroutine(kill());
    }

    override protected void moveUpdate(){
        Vector3 mov = new Vector3(Mathf.Cos(Time.fixedTime)*0.7f, Mathf.Sin(Time.fixedTime)*0.7f, 0);
        Vector3 des = transform.position + mov * Time.deltaTime;
        transform.position = des; 
    }

    private IEnumerator kill(){
        Debug.Log(currentHealth);
        yield return new WaitForSeconds(Random.Range(0.1f,3f));
        Shoot();
        StartCoroutine(kill());
    }

}
