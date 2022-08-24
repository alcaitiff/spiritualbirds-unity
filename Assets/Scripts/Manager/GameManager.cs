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
  
  [SerializeField]
  private GameObject prefabHeal;
  [SerializeField]
  private GameObject prefabPowerUp;
  [SerializeField]
  private PlayerController prefabPlayer;

  public List<EnemyStats> stats = new List<EnemyStats>();

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
    for(int i = 0;i<6;i++){
      stats.Add(new EnemyStats(i));
    }
  }

  public void MainMenu(){
    if(Mathf.Approximately(Time.timeScale, 0.0f)){
      UI.MainMenu();
    }
  }

  public void StartGame(){
    Time.timeScale = 1.0f;
    for(int i = 0;i<6;i++){
      stats[i].clear();
    }

    SceneManager.LoadScene((int)SceneIndexes.FASE_1, LoadSceneMode.Single);
  }

  public PlayerController startPlayer(Vector3 position){
    if(player == null){
      // TODO:Verificar como fazer entre fases
      PlayerController player = Instantiate(prefabPlayer, position, Quaternion.identity);
    }
    return player;
  }

  public void GamePause(){
    UI.GamePause();
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

  public void setScore(int value){
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

}
