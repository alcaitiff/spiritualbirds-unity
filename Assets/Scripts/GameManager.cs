using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public UIController UI;
  public PlayerController player;
  private void Awake()
  {
    if (GameManager.instance != null)
    {
      Destroy(gameObject);
      return;
    }
    instance = this;
    DontDestroyOnLoad(gameObject);
  }

  public void MainMenu(){
    if(Mathf.Approximately(Time.timeScale, 0.0f)){
      UI.MainMenu();
    }
  }

  public void StartGame(){
    Time.timeScale = 1.0f;
    SceneManager.LoadScene((int)SceneIndexes.FASE_1, LoadSceneMode.Single);
  }

  public void GamePause(){
    UI.GamePause();
  }

  public int powerInc(){
    return UI.powerWhell.Inc();
  }
  
  public int powerUp(){
    return UI.powerWhell.PowerUP();
  }

  public void setScore(int value){
    TextMeshProUGUI tm = UI.score.GetComponent<TextMeshProUGUI>();
    tm.text = value.ToString("D4");
  }

  public void setLife(int current,int max){
    if(current<1){
      SceneManager.LoadScene((int)SceneIndexes.TITLE, LoadSceneMode.Single);
    }else{
      UI.hp.setValue(current,max);
    }
  }
}
