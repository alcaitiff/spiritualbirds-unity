using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour,Hitable
{
    public bool active = true;
    private float time = 0f;
    [SerializeField]
    public AudioClip audioHit;
    [SerializeField]
    public AudioClip audioAppear;
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
                AudioSource.PlayClipAtPoint(audioAppear, new Vector3(0f,0f,-10f));
            }
        }
    }
    
    public int hit(int dmg){
        if(active){
            active=false;
            time=0f;
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetBool("Active", false);    
            AudioSource.PlayClipAtPoint(audioHit, new Vector3(0f,0f,-10f));
        }
        return 0;
    }

    public bool isDead(){
        return !active;
    }

}
