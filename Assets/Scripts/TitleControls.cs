using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TitleControls : MonoBehaviour
{
  // Start is called before the first frame update

  public GameManager gm;
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
    controls.GamePlay.Enable();
  }

  private void OnDisable() {
    controls.GamePlay.Disable();
  }

  public void StartGame(){
    gm.StartGame();
  }

}