using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TitleControls : MonoBehaviour
{
  // Start is called before the first frame update

  public GameManager gm;
  public GameObject menu;
  public int wait = 0;
  PlayerControls controls;


  void Awake(){
    controls = new PlayerControls();

    controls.GamePlay.Start.performed += ctx => StartGame();
    controls.GamePlay.Pause.performed += ctx => StartGame();
    controls.GamePlay.Shoot.performed += ctx => StartGame();
  }

  void Start(){
    gm = GameManager.instance;
  }

  private void OnEnable() {
    StartCoroutine(enableControls());
  }
  private IEnumerator enableControls(){
    yield return new WaitForSeconds(wait);
    controls.GamePlay.Enable();
  }

  private void OnDisable() {
    controls.GamePlay.Disable();
  }

  public void StartGame(){
    gm.StartGame();
  }

  public void ShowMenu(){
    if(menu){
      menu.SetActive(true);
    }
  }  
  
  public void HideMenu(){
    menu.SetActive(false);
  }

}