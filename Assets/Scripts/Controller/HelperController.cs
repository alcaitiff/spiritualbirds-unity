using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HelperController : MonoBehaviour
{

  public GameManager gm;
  
  [SerializeField]
  private FBController bulletPrefab;
  public Transform target;
  // Start is called before the first frame update
  void Start(){
    gm = GameManager.instance;
  }

  public void Shoot(){
    Vector3 offset = new Vector3(1f, 0, 0);
    Vector3 pos = transform.position + offset;
    FBController bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
    bullet.transform.localScale*=0.3f;
    bullet.setPlayer(gm.player);
    if(gm.player.getSpread()>1){
      ShootExtra(gm.player.getSpread()-1);
    }
  }
  
  public void ShootExtra(int num){
      Vector3 offset = new Vector3(1f, 0, 0) + transform.position;
      if(num>0){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.4f, 0), Quaternion.identity).setPlayer(gm.player).transform.localScale*=0.2f;}
      if(num>1){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.4f, 0), Quaternion.identity).setPlayer(gm.player).transform.localScale*=0.2f;}
      if(num>2){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(15f).transform.localScale*=0.1f;}
      if(num>3){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(-15f).transform.localScale*=0.1f;}      
      if(num>4){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(30f).transform.localScale*=0.1f;}
      if(num>5){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(-30f).transform.localScale*=0.1f;}      
      if(num>6){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(45f).transform.localScale*=0.1f;}
      if(num>7){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(-45f).transform.localScale*=0.1f;}
      if(num>8){Instantiate(bulletPrefab, offset+new Vector3(0f, 0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(60f).transform.localScale*=0.1f;}
      if(num>9){Instantiate(bulletPrefab, offset+new Vector3(0f, -0.6f, 0), Quaternion.identity).setPlayer(gm.player).setAngle(-60f).transform.localScale*=0.1f;}
  }
  
  void Update(){
    //transform.position = gm.player.transform.position + new Vector3(Random.Range(-40f,-30f),Random.Range(-40f,-30f),0f)* Time.deltaTime;
    GetComponent<Animator>().SetInteger("Lift", (int)Mathf.Round((target.position.y-transform.position.y)));
    transform.position += (target.position-transform.position)* Time.deltaTime*1.5f;
  }

}
