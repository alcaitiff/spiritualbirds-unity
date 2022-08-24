using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
  public GameObject menu;
  
  public void ShowMenu(){
    if(menu){
      menu.SetActive(true);
    }
  }  
  
  public void HideMenu(){
    menu.SetActive(false);
  }
}
