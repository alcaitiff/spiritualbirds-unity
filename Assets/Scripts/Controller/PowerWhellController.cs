using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerWhellController : MonoBehaviour
{
  public PowerUpUIController[] powerUps;
  private PowerUpUIController active;
  public int value = -1;
  // Start is called before the first frame update
  void Start()
  {

  }

  public int PowerUP()
  {
    int ret = value;
    deactivate();
    value = -1;
    return ret;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public int Inc()
  {
    value = (value < powerUps.Length - 1) ? value + 1 : 0;
    activate();
    return value;
  }

  private void activate()
  {
    deactivate();
    active = powerUps[value];
    active.Activate();
  }


  private void deactivate()
  {
    if (active != null)
    {
      active.DeActivate();
      active = null;
    }
  }
}
