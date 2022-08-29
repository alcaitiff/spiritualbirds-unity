using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Fase3Manager : Fase1Manager
{
    private bool superSelected = false;
    override protected void Start()
    {
        gm = GameManager.instance;
        bossStats = gm.stats[(int)EnemyIndexes.DEVILJAY];
        bossIndex = 1;
        nextFase=(int)SceneIndexes.FASE_3;
        done=false;
        boss=false;
        gm.UI=UI;
        UI.showSuperSelection();
        
        //only for tests
        //gm.stats[(int)EnemyIndexes.PIDGEON].born=120;
        //bossStats.killed=1;
        //boss=true;
    }

    override protected void Update(){
        if(!superSelected && gm.getSuper().Count>1){
            superSelected=true;
            gm.startPlayer(new Vector3(-5,3,0));
            StartCoroutine(UI.setScroller(true,0f));
        }
        if(superSelected){
            EnemyStats pidgeonStats = gm.stats[(int)EnemyIndexes.PIDGEON];
            if(pidgeonStats.born<220){
                enableEnemies();        
            }else if(!boss){
                enableBoss();
            }else if(bossStats.killed>0 && !done){
                done=true;
                StartCoroutine(goToFase(nextFase));
            }
        }
    }

    override protected void enableEnemies(){
        EnemyStats pidgeonStats = gm.stats[(int)EnemyIndexes.PIDGEON];
        if(!pidgeon.active){
            pidgeon.setInterval(4f);
            pidgeon.enable();
        }else if(!woodpecker.active && pidgeonStats.born>126 && pidgeonStats.born<132){
            pidgeon.setInterval(2f);
        }else if(!woodpecker.active && pidgeonStats.born>132){
            pidgeon.setInterval(4f);
            woodpecker.setInterval(6f);
            woodpecker.enable();
        }else if(!hawk.active && pidgeonStats.born>138){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(7f);
            hawk.setInterval(6f);
            hawk.enable();
        }else if(!blueJay.active && pidgeonStats.born>144){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(8f);
            hawk.setInterval(7f);
            blueJay.setInterval(9f);
            blueJay.enable();
        }else if(!orangeBird.active && pidgeonStats.born>160){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(8f);
            hawk.setInterval(7f);
            blueJay.setInterval(10f);
            orangeBird.setInterval(4f);
            orangeBird.enable();     
        }else if(!crow.active && pidgeonStats.born>200){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(9f);
            hawk.setInterval(8f);
            orangeBird.setInterval(10f);
            blueJay.setInterval(15f);
            crow.setInterval(4f);
            crow.enable();
        }
    }

}
