using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.UI=UI;
        pidgeon.setInterval(4f);
        pidgeon.enable();
        blueJay.setInterval(2f);
        blueJay.enable();
        
        gm.startPlayer(new Vector3(-5,3,0));
    }

    void Update(){
        EnemyStats pidgeonStats = gm.stats[(int)EnemyIndexes.PIDGEON];
        if(!woodpecker.active && pidgeonStats.killed+pidgeonStats.slipped>6){
            pidgeon.setInterval(4f);
            woodpecker.setInterval(6f);
            woodpecker.enable();
        }
        if(!hawk.active && pidgeonStats.killed+pidgeonStats.slipped>12){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(7f);
            hawk.setInterval(6f);
            hawk.enable();
        }
    }


}
