using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public GameObject score;
    public PowerWhellController powerWhell;
    public ProgressBarController hp;
    public PauseButton pauseButton;

    public void GamePause(){
        pauseButton.TogglePause();
    }
    
    public void MainMenu(){
        SceneManager.LoadScene((int)SceneIndexes.TITLE, LoadSceneMode.Single);
    }
}
