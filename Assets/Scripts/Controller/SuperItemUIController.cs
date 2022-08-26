using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperItemUIController : MonoBehaviour{
    public GameManager gm;
    public int index;
    void Start(){
        gm = GameManager.instance;
    }
    public void select(){
        gm.selectSuper(index);
    }
}
