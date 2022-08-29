using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{
    public GameObject score;
    public PowerWheelController powerWheel;
    public GameObject bullets;
    public DynamicJoystick dynamicJoystick;
    public ProgressBarController hp;
    public PauseButton pauseButton;
    public GameManager gm;
    public GameObject congratulations;
    public GameObject fireButton;
    public SuperItemUIController buffPrefab;
    public SuperSelectionController superSelection;
    [SerializeField]
    public List<GameObject> bosses = new List<GameObject>();
    public TextMeshProUGUI tutorial;
    public AudioClip bossMusic;
    public List<BackgroundTimedScroller> backgrounds = new List<BackgroundTimedScroller>();
    private Coroutine routine;
    private float volume = 0.6f;
    private bool tutorialActive = true;
    [SerializeField]
    public AudioClip audioSelectSuper;

    // Start is called before the first frame update
    void Start(){
        gm = GameManager.instance;
        showTutorial(gm.tutorial);
        setVirtualButton(gm.virtualButon);
    }

    public void GamePause(){
        pauseButton.TogglePause();
    }
    
    public void MainMenu(){
        SceneManager.LoadScene((int)SceneIndexes.TITLE, LoadSceneMode.Single);
    }

    public void Shoot(){
        gm.Shoot();
    }        
    
    public void StartShoot(){
        gm.StartShoot();
    }

    public void StopShoot(){
        gm.StopShoot();
    }
  

    public void PowerUp(){
        gm.player.PowerUp();
    }    
    
    public void setVirtualButton(bool value){
        fireButton.SetActive(value);
    }

    public void showTutorial(bool value){
        tutorialActive=value;
        tutorial.gameObject.SetActive(tutorialActive);    
    }

    public void setTutorial(string text, float time=6f){
        tutorial.text=text;
        tutorial.gameObject.SetActive(tutorialActive);
        if(routine!=null){
            StopCoroutine(routine);
        }
        routine = StartCoroutine(hideTutorial(time));
    }

    private IEnumerator hideTutorial(float time){
        yield return new WaitForSeconds(time);
        tutorial.text="";
        tutorial.gameObject.SetActive(false);
    }

    public void setUpBoss(int index){
        StartCoroutine(GetComponent<AudioSource>().CrossFade(
                        newSound: bossMusic,
                        finalVolume: volume,
                        fadeTime: 1f));
        StartCoroutine(activateBossBattle(index));
    }
    private IEnumerator activateBossBattle(int index){
        yield return new WaitForSeconds(3f);
        StartCoroutine(setScroller(false));
        yield return new WaitForSeconds(4f);
        bosses[index].gameObject.SetActive(true);
    }

    public IEnumerator setScroller(bool value, float time=0.2f){
        foreach(BackgroundTimedScroller background in backgrounds){
            yield return new WaitForSeconds(time);
            background.stop=!value;
            if(value){
                background.scrollSpeed=0.5f;
            }
        }
    }

    public void showSuperSelection(){
        superSelection.gameObject.SetActive(true);
        superSelection.getSuperIndexes(gm.getSuper());
        setBasicUIVisibility(false);
    }    
    
    public void showCongratulations(){
        congratulations.SetActive(true);
        setBasicUIVisibility(false);
    }

    public void hideSuperSelection(){
        AudioSource.PlayClipAtPoint(audioSelectSuper, new Vector3(0f,0f,-10f));
        setBasicUIVisibility(true);
        updateUIForSuper();
        superSelection.clear();
        superSelection.gameObject.SetActive(false);
    }

    private void setBasicUIVisibility(bool value){
        powerWheel.gameObject.SetActive(value);
        hp.gameObject.SetActive(value);
        bullets.SetActive(value);
    }

    public void updateUIForSuper(){
        List<int> s = gm.getSuper();
        for(int i=0;i<s.Count;i++){
            SuperItemUIController item;
            int index=s[i];
            switch (index){
                case (int)SuperIndexes.AUTO:
                    powerWheel.powerUps[(int)PowerIndexes.AMMO].setSprite((int)SuperIndexes.AUTO);
                    break;
                case (int)SuperIndexes.PIERCE:
                    powerWheel.powerUps[(int)PowerIndexes.DMG].setSprite((int)SuperIndexes.PIERCE);
                    break;
                case (int)SuperIndexes.STAR:
                    powerWheel.powerUps[(int)PowerIndexes.SPREAD].setSprite((int)SuperIndexes.STAR);
                    break;
                case (int)SuperIndexes.ORBITAL:
                    powerWheel.powerUps[(int)PowerIndexes.SPEED].setSprite((int)SuperIndexes.ORBITAL);
                    break;
                case (int)SuperIndexes.BOUNCE:
                case (int)SuperIndexes.PULSE:
                case (int)SuperIndexes.REGEN:
                case (int)SuperIndexes.SHIELD:
                default:
                    item = Instantiate(buffPrefab, transform) as SuperItemUIController;
                    item.transform.position+=new Vector3(i*-1f,0,0);
                    item.index = index;
                    item.updateTexture();
                    break;
            }
        }
    }
}
