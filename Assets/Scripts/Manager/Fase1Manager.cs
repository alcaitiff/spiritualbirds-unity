using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fase1Manager : MonoBehaviour
{
    public GameManager gm;
    public UIController UI;
    [SerializeField]
    private Spawner pidgeon;
    [SerializeField]
    private Spawner woodpecker;    
    [SerializeField]
    private Spawner hawk;    
    [SerializeField]
    private Spawner blueJay;
    [SerializeField]
    private Spawner orangeBird;    
    [SerializeField]
    private Spawner crow;
    private bool boss = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.UI=UI;
        gm.startPlayer(new Vector3(-5,3,0));
    }

    private IEnumerator initialTutorial(){
        yield return new WaitForSeconds(8f);
        UI.setTutorial("Use your finger on the left side to move \nArrow keys and joysticks are supported too");
        yield return new WaitForSeconds(8f);
        UI.setTutorial("Use the button to shoot\nOn keyboards use 'Q'");
        yield return new WaitForSeconds(8f);
        UI.setTutorial("Get the spirit fires to powerup");
        yield return new WaitForSeconds(8f);
        UI.setTutorial("Touch the powerwheel to consume it\nOn keyboards use 'W'");
        yield return new WaitForSeconds(8f);
        UI.setTutorial("Good luck!");
    }

    void Update(){
        EnemyStats pidgeonStats = gm.stats[(int)EnemyIndexes.PIDGEON];
        EnemyStats bossStats = gm.stats[(int)EnemyIndexes.DEVILEON];
        if(pidgeonStats.born<60){
            enableEnemies();        
        }else if(!boss){
            enableBoss();
        }else if(bossStats.killed>0){
            //Next fase
        }
    }
    private void enableBoss(){
        boss=true;
        UI.setUpBoss();
        StartCoroutine(disableEnemies());
    }
    private void enableEnemies(){
        EnemyStats pidgeonStats = gm.stats[(int)EnemyIndexes.PIDGEON];
        if(!pidgeon.active && pidgeonStats.born==0){
            pidgeon.setInterval(4f);
            pidgeon.enable();
            StartCoroutine(initialTutorial());
        }else if(!woodpecker.active && pidgeonStats.born>6 && pidgeonStats.born<12){
            pidgeon.setInterval(2f);
        }else if(!woodpecker.active && pidgeonStats.born>12){
            pidgeon.setInterval(4f);
            woodpecker.setInterval(6f);
            woodpecker.enable();
            UI.setTutorial("Some birds need more than one shoot to be defeated");     
        }else if(!hawk.active && pidgeonStats.born>18){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(7f);
            hawk.setInterval(6f);
            hawk.enable();
            UI.setTutorial("Increase your life or you may die in one hit to strong enemies");   
        }else if(!blueJay.active && pidgeonStats.born>24){
            pidgeon.setInterval(6f);
            woodpecker.setInterval(8f);
            hawk.setInterval(7f);
            blueJay.setInterval(9f);
            blueJay.enable();
        }else if(!orangeBird.active && pidgeonStats.born>30){
            pidgeon.setInterval(6f);
            woodpecker.setInterval(8f);
            hawk.setInterval(7f);
            blueJay.setInterval(10f);
            orangeBird.setInterval(4f);
            orangeBird.enable();     
        }else if(!crow.active && pidgeonStats.born>40){
            pidgeon.setInterval(7f);
            woodpecker.setInterval(9f);
            hawk.setInterval(8f);
            orangeBird.setInterval(10f);
            blueJay.setInterval(15f);
            crow.setInterval(4f);
            crow.enable();
        }
    }
    private IEnumerator disableEnemies(float time = 5f){
        yield return new WaitForSeconds(time);
        pidgeon.disable();
        woodpecker.disable();
        hawk.disable();
        orangeBird.disable();
        blueJay.disable();
        crow.disable();
    }

}
