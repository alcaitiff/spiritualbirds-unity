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
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.UI=UI;
        gm.startPlayer(new Vector3(-5,3,0));
    }

    private IEnumerator initialTutorial(){
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Use your finger on the left side to move \n\nArrow keys an joysticks are supported too");
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Don't let the evil birds to touch you");
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Use the button to fire\n\nOn keyboards use 'Q'");
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Get the spirit fires to powerup");
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Touch the powerwheel to use it\n\nOn keyboards use 'W'");
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("The powerups available are:\n\nspeed, ammunition, max HP, spread, damage and helpers");
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Some birds need more than one shoot to be defeated");        
        yield return new WaitForSeconds(5.5f);
        UI.setTutorial("Good luck!");
    }

    void Update(){
        EnemyStats pidgeonStats = gm.stats[(int)EnemyIndexes.PIDGEON];
        if(!pidgeon.active && pidgeonStats.born==0){
            pidgeon.setInterval(4f);
            pidgeon.enable();
            StartCoroutine(initialTutorial());
        }else if(!woodpecker.active && pidgeonStats.born>6){
            pidgeon.setInterval(4f);
            woodpecker.setInterval(6f);
            woodpecker.enable();
        }else if(!hawk.active && pidgeonStats.born>12){
            pidgeon.setInterval(5f);
            woodpecker.setInterval(7f);
            hawk.setInterval(6f);
            hawk.enable();
            UI.setTutorial("Increase your life or you may die in one hit to strong enemies");   
        }else if(!blueJay.active && pidgeonStats.born>18){
            pidgeon.setInterval(6f);
            woodpecker.setInterval(8f);
            hawk.setInterval(7f);
            blueJay.setInterval(9f);
            blueJay.enable();
        }else if(!orangeBird.active && pidgeonStats.born>24){
            pidgeon.setInterval(6f);
            woodpecker.setInterval(8f);
            hawk.setInterval(7f);
            orangeBird.setInterval(4f);
            orangeBird.enable();     
        }
        //crow.setInterval(4f);
        //crow.enable();
    }


}
