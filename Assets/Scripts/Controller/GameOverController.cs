using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI pidgeonScore;
    public TextMeshProUGUI woodpeeckerScore;
    public TextMeshProUGUI hawkScore;
    public TextMeshProUGUI blueJayScore;
    public TextMeshProUGUI orangeBirdScore;
    public TextMeshProUGUI crowScore;
    public TextMeshProUGUI score;
    public GameManager gm;
  
    void Start(){
        gm = GameManager.instance;
        score.text=gm.player.score.ToString("D5");
        pidgeonScore.text=getScore((int)EnemyIndexes.PIDGEON);
        woodpeeckerScore.text=getScore((int)EnemyIndexes.WOODPECKER);
        hawkScore.text=getScore((int)EnemyIndexes.HAWK);
        blueJayScore.text=getScore((int)EnemyIndexes.BLUEJAY);
        orangeBirdScore.text=getScore((int)EnemyIndexes.ORANGE_BIRD);
        crowScore.text=getScore((int)EnemyIndexes.CROW);
    }

    private string getScore(int i){
        return gm.stats[i].killed.ToString("D3")+"/"+gm.stats[i].born.ToString("D3");
    }
    
}
