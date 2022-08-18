using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
  // Start is called before the first frame update

  public Animator animator;
  public DynamicJoystick joystick;
  private float minX = -7.5f;
  private float maxX = 7.5f;
  private float minY = -4.5f;
  private float maxY = 4.5f;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    float v = joystick.Vertical;
    float h = joystick.Horizontal;
    float dy = Mathf.Abs(v) < 0.1f ? (Mathf.Abs(h) < 0.1f ? -1f : 0) : (v > 0 ? 3f : -4f);
    //Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
    //float dy = Input.GetAxis("Vertical") == 0 ? -0.5f : (Input.GetAxis("Vertical") > 0 ? 1.5f : -2f);

    animator.SetInteger("Lift", (int)Mathf.Round(dy));

    Vector3 mov = new Vector3(3 * h, dy, 0);
    Vector3 des = transform.position + mov * Time.deltaTime;
    float x = des.x > minX ? (des.x < maxX ? des.x : maxX) : minX;
    float y = des.y > minY ? (des.y < maxY ? des.y : maxY) : minY;
    transform.position = new Vector3(x, y, des.z);

  }
}