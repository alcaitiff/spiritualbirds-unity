using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public bool virtualButon = true;
  public bool sound = true;
  public bool tutorial = true;
  public UIController UI;
  
  [SerializeField]
  private GameObject prefabHeal;
  [SerializeField]
  private GameObject prefabPowerUp;
  [SerializeField]
  private PlayerController prefabPlayer;
  public List<EnemyStats> stats = new List<EnemyStats>();
  private List<int> super;
  public PlayerController player;
  private int numEnemies;
  public int score;
  private void Awake()
  {
    if (GameManager.instance != null)
    {
      Destroy(gameObject);
      return;
    }
    instance = this;
    loadPreferences();
    DontDestroyOnLoad(gameObject);
    numEnemies = Enum.GetNames(typeof(EnemyIndexes)).Length;
    for(int i = 0;i<numEnemies;i++){
      stats.Add(new EnemyStats(i));
    }
  }

  public void savePreferences(){
    PlayerPrefs.SetInt("virtualButon", virtualButon?1:0);
    PlayerPrefs.SetInt("sound", sound?1:0);
    PlayerPrefs.SetInt("tutorial", tutorial?1:0);
    PlayerPrefs.Save();
  }
 
  public void loadPreferences(){
    virtualButon = PlayerPrefs.GetInt("virtualButon", 1)==0?false:true;
    sound = PlayerPrefs.GetInt("sound", 1)==0?false:true;
    tutorial = PlayerPrefs.GetInt("tutorial", 1)==0?false:true;
    AudioListener.volume = sound?1:0;
  }

  public void MainMenu(){
    if(Mathf.Approximately(Time.timeScale, 0.0f)){
      UI.MainMenu();
    }
  }

  public void StartGame(){
    Time.timeScale = 1.0f;
    for(int i = 0;i<numEnemies;i++){
      stats[i].clear();
    }
    score=0;
    super = new List<int>();

    SceneManager.LoadScene((int)SceneIndexes.FASE_1, LoadSceneMode.Single);
  }

  public PlayerController startPlayer(Vector3 position){
    if(player == null){
      player = Instantiate(prefabPlayer, position, Quaternion.identity) as PlayerController;
    }
    foreach(int index in super){
      player.addSuper(index);
    }
    player.score=score;
    setScore(score);

    return player;
  }

  public void GamePause(){
    UI.GamePause();
  }  
  
  public bool ToogleVirtualButton(){
    virtualButon=!virtualButon;
    if(UI){
      UI.setVirtualButton(virtualButon);
    }
    savePreferences();
    return virtualButon;
  }

  public bool ToogleSound(){
    sound=!sound;
    AudioListener.volume = sound?1:0;
    savePreferences();
    return sound;
  }  
  
  public bool ToogleTutorial(){
    tutorial=!tutorial;
     if(UI){
      UI.showTutorial(tutorial);
    }
    savePreferences();
    return tutorial;
  }

  public int powerInc(){
    return UI.powerWheel.Inc();
  }
  
  public int powerUp(){
    return UI.powerWheel.PowerUP();
  }  
  
  public void Shoot(){
    player.Shoot();
  }
  
  public void StartShoot(){
    player.StartShoot();
  }

  public void StopShoot(){
    player.StopShoot();
  }

  public void setScore(int value){
    score=value;
    TextMeshProUGUI tm = UI.score.GetComponent<TextMeshProUGUI>();
    tm.text = value.ToString("D4");
  }

  public void setLife(int current,int max){
    if(current<1){
      SceneManager.LoadScene((int)SceneIndexes.GAMEOVER, LoadSceneMode.Single);
    }else{
      UI.hp.setValue(current,max);
    }
  }

  public void spawnHeal(Vector3 position){
    Instantiate(prefabHeal, position, Quaternion.identity);
  }
  
  public void spawnPowerUp(Vector3 position){
    Instantiate(prefabPowerUp, position, Quaternion.identity);
  }

  public void selectSuper(int index){
    super.Add(index);
    UI.hideSuperSelection();
  }

  public List<int> getSuper(){
    return super;
  }
  
}
