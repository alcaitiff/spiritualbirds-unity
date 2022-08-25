using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
  public GameManager gm;
  private Animator animator;
  private DynamicJoystick joystick;
  private PlayerController player;
  private Vector2 move = Vector2.zero;
  private PlayerControls controls;
  private float minX = -7.5f;
  private float maxX = 7.5f;
  private float minY = -4.5f;
  private float maxY = 4.5f;
  void Awake(){
    controls = new PlayerControls();

    controls.GamePlay.MoveUp.performed += ctx => move.y   += 1f;
    controls.GamePlay.MoveLeft.performed += ctx => move.x += -1f;
    controls.GamePlay.MoveDown.performed += ctx => move.y += -1f;
    controls.GamePlay.MoveRight.performed += ctx => move.x+= 1f;
    controls.GamePlay.MoveUp.canceled += ctx => move.y   -= 1f;
    controls.GamePlay.MoveLeft.canceled += ctx => move.x -= -1f;
    controls.GamePlay.MoveDown.canceled += ctx => move.y -= -1f;
    controls.GamePlay.MoveRight.canceled += ctx => move.x-= 1f;

    controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
    controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;    
    
    controls.GamePlay.Start.performed += ctx => MainMenu();
    controls.GamePlay.Pause.performed += ctx => GamePause();
    controls.GamePlay.PowerUp.performed += ctx => PowerUp();
    //controls.GamePlay.Shoot.performed += ctx => Shoot();
    controls.GamePlay.Shoot.started += ctx => StartShoot();
    controls.GamePlay.Shoot.canceled += ctx => StopShoot();
   
  }

  void Start(){
    gm = GameManager.instance;
    animator = gameObject.GetComponent<Animator>();
    player = gameObject.GetComponent<PlayerController>();
    joystick = gm.UI.dynamicJoystick;
  }

  private void OnEnable() {
    controls.GamePlay.Enable();
  }

  private void OnDisable() {
    controls.GamePlay.Disable();
  }

  void PowerUp(){
    player.PowerUp();
  }

  public void StartShoot(){
    player.StartShoot();
  }

  public void StopShoot(){
    player.StopShoot();
  }

  void GamePause(){
    gm.GamePause();
  }

  void MainMenu(){
    gm.MainMenu();
  }

  void Update(){
    float s = player.getSpeed()*0.3f;
    float v = Mathf.Approximately(joystick.Vertical, 0.0f) ? move.y:joystick.Vertical;
    v=Mathf.Approximately(v, 0.0f)?0f:(v>0f?v+s:v-s);
    float h = Mathf.Approximately(joystick.Horizontal, 0.0f) ? move.x:joystick.Horizontal;
    float dy = Mathf.Approximately(v, 0.0f) ? (Mathf.Approximately(h, 0.0f) ? -1f : 0) : v ;
    h=Mathf.Approximately(h, 0.0f)?0f:(h>0f?h+s:h-s);
    animator.SetInteger("Lift", (int)Mathf.Round(dy));
    Vector3 mov = new Vector3(h, dy, 0);
    Vector3 des = transform.position + mov * Time.deltaTime;
    float x = des.x > minX ? (des.x < maxX ? des.x : maxX) : minX;
    float y = des.y > minY ? (des.y < maxY ? des.y : maxY) : minY;
    transform.position = new Vector3(x, y, des.z);
  }
}