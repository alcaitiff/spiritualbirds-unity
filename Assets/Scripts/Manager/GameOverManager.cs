using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public TextMeshProUGUI pidgeonScore;
    public TextMeshProUGUI woodpeeckerScore;
    public TextMeshProUGUI hawkScore;
    public TextMeshProUGUI blueJayScore;
    public TextMeshProUGUI orangeBirdScore;
    public TextMeshProUGUI crowScore;
    public TextMeshProUGUI score;
    public TextMeshProUGUI devileon;
    public TextMeshProUGUI devilJay;
    public TextMeshProUGUI devilBaron;
    public GameManager gm;
  
    void Start(){
        gm = GameManager.instance;
        score.text=gm.score.ToString("D5");
        pidgeonScore.text=getScore((int)EnemyIndexes.PIDGEON);
        woodpeeckerScore.text=getScore((int)EnemyIndexes.WOODPECKER);
        hawkScore.text=getScore((int)EnemyIndexes.HAWK);
        blueJayScore.text=getScore((int)EnemyIndexes.BLUEJAY);
        orangeBirdScore.text=getScore((int)EnemyIndexes.ORANGE_BIRD);
        crowScore.text=getScore((int)EnemyIndexes.CROW);
        devileon.text=getScore((int)EnemyIndexes.DEVILEON);
        devilJay.text=getScore((int)EnemyIndexes.DEVILJAY);
        devilBaron.text=getScore((int)EnemyIndexes.DEVILBARON);
        disableObjects();
        StartCoroutine(enableObjects());
    }

    private void disableObjects(){
        foreach(GameObject g in objects){
            g.SetActive(false);
        }
    }

    private IEnumerator enableObjects(){
        for(int i =0;i<objects.Count;i++){
            if(i>8 || gm.stats[i].born>0){
                yield return new WaitForSeconds(0.5f);
                objects[i].SetActive(true);
            }
        }
    }

    private string getScore(int i){
        if(i<6){
            return gm.stats[i].killed.ToString("D3")+"/"+gm.stats[i].born.ToString("D3");
        }else{
            return gm.stats[i].killed.ToString()+"/"+gm.stats[i].born.ToString();
        }
    }
    
}
