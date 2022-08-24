using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{
    public GameObject score;
    public PowerWheelController powerWheel;
    public DynamicJoystick dynamicJoystick;
    public ProgressBarController hp;
    public PauseButton pauseButton;
    public GameManager gm;
    public GameObject fireButton;
    public TextMeshProUGUI tutorial;
    private Coroutine routine;
    private bool tutorialActive = true;

    // Start is called before the first frame update
    void Start(){
        gm = GameManager.instance;
        setVirtualButton(gm.virtualButon);
    }

    public void GamePause(){
        pauseButton.TogglePause();
    }
    
    public void MainMenu(){
        SceneManager.LoadScene((int)SceneIndexes.TITLE, LoadSceneMode.Single);
    }

    public void Shoot(){
        gm.Shoot();
    }        
    
    public void PowerUp(){
        gm.player.PowerUp();
    }    
    
    public void setVirtualButton(bool value){
        fireButton.SetActive(value);
    }

    public void showTutorial(bool value){
        tutorialActive=value;
        tutorial.gameObject.SetActive(tutorialActive);    
    }

    public void setTutorial(string text, float time=5f){
        tutorial.text=text;
        tutorial.gameObject.SetActive(tutorialActive);
        if(routine!=null){
            StopCoroutine(routine);
        }
        routine = StartCoroutine(hideTutorial(time));
    }

    private IEnumerator hideTutorial(float time){
        yield return new WaitForSeconds(time);
        tutorial.text="";
        tutorial.gameObject.SetActive(false);
    }
}
