using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour,Hitable
{
    public bool active = true;
    private float time = 0f;
    private float delay = 5f;
    void Update()
    {
        if(!active){
            time+=Time.deltaTime;
            if(time>=delay){
                active=true;
                time=0;
                Animator animator = gameObject.GetComponent<Animator>();
                animator.SetBool("Active", true);    
                //playsound
            }
        }
    }
    
    public int hit(int dmg){
        if(active){
            active=false;
            time=0f;
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetBool("Active", false);    
            //playsound
        }
        return 0;
    }

    public bool isDead(){
        return !active;
    }

}
