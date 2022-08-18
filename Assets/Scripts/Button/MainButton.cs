using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainButton : MonoBehaviour
{
  public void MainMenu()
  {
    SceneManager.LoadScene((int)SceneIndexes.TITLE, LoadSceneMode.Single);
  }
}
