using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public ProgressBar progressBar;
    private AsyncOperation operation;

    // Start is called before the first frame update
    void Start()
    {
        operation = SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE, LoadSceneMode.Single);
        progressBar.setOperation(operation);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
