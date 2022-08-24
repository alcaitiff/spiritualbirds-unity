using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public GameObject score;
    public PowerWheelController powerWheel;
    public DynamicJoystick dynamicJoystick;
    public ProgressBarController hp;
    public PauseButton pauseButton;
    public GameManager gm;
    public GameObject fireButton;
  
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
}
