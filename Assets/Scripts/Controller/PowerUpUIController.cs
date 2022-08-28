using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUIController : MonoBehaviour
{
  public bool active = false;
  private SpriteRenderer spriteRenderer;
  public List<Sprite> superSprites = new List<Sprite>();
  void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    float amount = active ? 0f : 1f;
    spriteRenderer.material.SetFloat("_GrayscaleAmount",amount);
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
  public void setSprite(int index){
     spriteRenderer.sprite=superSprites[index];
     transform.localScale*=0.5f;
  }
}
