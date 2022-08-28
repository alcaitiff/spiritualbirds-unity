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
    public GameObject fireButton;
    public SuperItemUIController buffPrefab;
    public SuperSelectionController superSelection;
    [SerializeField]
    public List<GameObject> bosses = new List<GameObject>();
    public TextMeshProUGUI tutorial;
    public AudioClip bossMusic;
    public List<BackgroundTimedScroller> backgrounds = new List<BackgroundTimedScroller>();
    private Coroutine routine;
    private float volume = 0.1f;
    private bool tutorialActive = true;

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
                        fadeTime: 3f));
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
        superSelection.getSuperIndexes();
        setBasicUIVisibility(false);
    }

    public void hideSuperSelection(int index){
        updateUIForSuper(index);
        setBasicUIVisibility(true);
        superSelection.clear();
        superSelection.gameObject.SetActive(false);
    }

    private void setBasicUIVisibility(bool value){
        powerWheel.gameObject.SetActive(value);
        hp.gameObject.SetActive(value);
        bullets.SetActive(value);
    }

    public void updateUIForSuper(int index){
        SuperItemUIController item;
        switch (index){
            case (int)SuperIndexes.AUTO:
            break;
            case (int)SuperIndexes.BOUNCE:
            break;
            case (int)SuperIndexes.ORBITAL:
            break;
            case (int)SuperIndexes.PIERCE:
            break;
            case (int)SuperIndexes.PULSE:
            break;
            case (int)SuperIndexes.REGEN:
                item = Instantiate(buffPrefab, transform) as SuperItemUIController;
                item.index = index;
                item.updateTexture();
            break;
            case (int)SuperIndexes.SHIELD:
            break;
            case (int)SuperIndexes.STAR:
                powerWheel.powerUps[(int)PowerIndexes.SPREAD].setSprite((int)SuperIndexes.STAR);
            break;
            default:
            break;
        }
    }
}
