using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
 
    public GameManager gm;
    private bool sound;
    private bool virtualButon;
    public GameObject virtualButonChecked;
    public GameObject virtualButonUnChecked;
    public GameObject soundChecked;
    public GameObject soundUnChecked;

    void Start(){
        gm = GameManager.instance;
        sound=gm.sound;
        virtualButon=gm.virtualButon;
        toogleGameObjectSound();
        toogleGameObjectVirtualButton();
    }

    public void closeMenu(){
        this.gameObject.SetActive(false);
    }

    private void toogleGameObjectSound(){
        soundChecked.SetActive(sound);
        soundUnChecked.SetActive(!sound);
    }

    private void toogleGameObjectVirtualButton(){
        virtualButonChecked.SetActive(virtualButon);
        virtualButonUnChecked.SetActive(!virtualButon);
    }

    public void ToogleSound(){
        sound=gm.ToogleSound();
        toogleGameObjectSound();
    }

    public void ToogleVirtualButton(){
        virtualButon=gm.ToogleVirtualButton();
        toogleGameObjectVirtualButton();
    }
}
