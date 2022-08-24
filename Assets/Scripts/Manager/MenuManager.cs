using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
 
    public GameManager gm;
    private bool sound;
    private bool virtualButon;
    private bool tutorial;
    public GameObject virtualButonChecked;
    public GameObject virtualButonUnChecked;
    public GameObject soundChecked;
    public GameObject soundUnChecked;    
    public GameObject tutorialChecked;
    public GameObject tutorialUnChecked;

    void Start(){
        gm = GameManager.instance;
        sound=gm.sound;
        virtualButon=gm.virtualButon;
        tutorial=gm.tutorial;
        toogleGameObjectSound();
        toogleGameObjectTutorial();
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
    
    private void toogleGameObjectTutorial(){
        tutorialChecked.SetActive(tutorial);
        tutorialUnChecked.SetActive(!tutorial);
    }

    public void ToogleTutorial(){
        tutorial=gm.ToogleTutorial();
        toogleGameObjectTutorial();
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
