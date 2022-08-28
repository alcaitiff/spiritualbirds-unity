using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gray : MonoBehaviour{
  public float effectAmount;
  private MaterialPropertyBlock propertyBlock;
  private SpriteRenderer spriteRenderer;
  private void Awake(){
    spriteRenderer = GetComponent<SpriteRenderer>();
    propertyBlock = new MaterialPropertyBlock();
  }
 
  private void Start(){
    if(spriteRenderer != null){
        spriteRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_EffectAmount", effectAmount);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }
  }
}