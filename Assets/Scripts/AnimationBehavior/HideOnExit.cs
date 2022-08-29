using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnExit : StateMachineBehaviour{
    private Animator animator;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        this.animator=animator;
        changeAlpha(0,stateInfo.length);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        this.animator=animator;
        animator.gameObject.GetComponent<SpriteRenderer>().color=new Color(1,1,1,40);    
    }

    private IEnumerator changeAlpha(float alpha, float time){
        yield return new WaitForSeconds(time);
        animator.gameObject.GetComponent<SpriteRenderer>().color=new Color(1,1,1,alpha);
    }
}
