using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

  private Slider slider;
  private Text textValue;

  private AsyncOperation operation;

  private void Start()
  {
    slider = gameObject.GetComponent<Slider>();
    textValue = gameObject.GetComponentInChildren<Text>(true);
    SetValue(slider.value);
  }
  public void SetValue(float value)
  {
    if (slider != null)
    {
      slider.value = value;
      textValue.text = Mathf.Round(value * 100 / slider.maxValue) + "%";
    }
  }

  public void setOperation(AsyncOperation op)
  {
    operation = op;
  }

  private void Update()
  {
    if (operation != null && !operation.isDone)
    {
      SetValue(operation.progress);
    }
    else
    {
      operation = null;
    }
  }
}
