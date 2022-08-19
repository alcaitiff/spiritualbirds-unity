using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPController : MonoBehaviour
{
    public AudioClip appear;
    public AudioClip hit;

    // Start is called before the first frame update
    void Start(){
        AudioSource.PlayClipAtPoint(appear, transform.position);   
    }

    virtual public void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetBool("Hit", true);        
            AudioSource.PlayClipAtPoint(hit, transform.position);
            PlayerController p = other.gameObject.GetComponent<PlayerController>();
            p.AddPower();
        };
    }
    // Update is called once per frame
    void Update(){
        Vector3 mov = new Vector3(-1f, 0, 0);
        Vector3 des = transform.position + mov * Time.deltaTime;
        transform.position = des;
        if(des.x<-12.5){
            Destroy(gameObject);
        }
    }

}
