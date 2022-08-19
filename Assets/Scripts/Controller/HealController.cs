using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : PowerUPController
{
    override public void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetBool("Hit", true);        
            AudioSource.PlayClipAtPoint(hit, transform.position);
            PlayerController p = other.gameObject.GetComponent<PlayerController>();
            p.Heal();
        };
    }

}
