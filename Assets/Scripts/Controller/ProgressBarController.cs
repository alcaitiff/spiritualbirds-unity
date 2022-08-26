using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class ProgressBarController : MonoBehaviour
{
  public int maximum;
  public int current;
  public Image mask;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    GetCurrentFill();
  }

  void GetCurrentFill()
  {
    float fillAmount = (float)current / (float)maximum;
    //mask.fillAmount = fillAmount;
    
    if(Mathf.Abs(mask.fillAmount-fillAmount)<0.003f){
      mask.fillAmount = fillAmount;
    }else{
      mask.fillAmount += (mask.fillAmount>fillAmount)?-0.003f:0.003f;
    }
      
  }

  public void setValue(int c,int m){
    maximum=m;
    current=c;
  }
}
