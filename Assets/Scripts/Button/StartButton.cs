using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene((int)SceneIndexes.FASE_1, LoadSceneMode.Single);
  }
}
