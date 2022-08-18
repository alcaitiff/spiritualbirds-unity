using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseButton : MonoBehaviour
{
    public GameObject caption;
    public GameObject UI;
    void Start() {
    }
    public void TogglePause() {
        if(Mathf.Approximately(Time.timeScale, 0.0f)){
            Time.timeScale = 1.0f;        
            caption.SetActive(false);
            UI.GetComponent<AudioSource>().Play();
        }else{
            Time.timeScale = 0.0f;        
            caption.SetActive(true);
            UI.GetComponent<AudioSource>().Pause();
        }
   }
}
