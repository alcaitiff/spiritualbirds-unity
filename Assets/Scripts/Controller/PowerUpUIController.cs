using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUIController : MonoBehaviour
{
  public bool active = false;
  private SpriteRenderer spriteRenderer;
  void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    int alpha = active ? 1 : 0;
    spriteRenderer.color = new Color(1, 1, 1, alpha);
  }

  public void Toggle()
  {
    active = !active;
  }
  public void Activate()
  {
    active = true;
  }
  public void DeActivate()
  {
    active = false;
  }
}
