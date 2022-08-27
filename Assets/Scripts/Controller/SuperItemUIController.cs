using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperItemUIController : MonoBehaviour{
    public GameManager gm;
    public int index = 0;
    public List<Sprite> sprites = new List<Sprite>();
    void Start(){
        gm = GameManager.instance;
    }
    public void select(){
        gm.selectSuper(index);
    }
    public void updateTexture(){
        gameObject.GetComponent<Image>().sprite=sprites[index];
    }
}
