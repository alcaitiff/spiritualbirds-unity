using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fase1Manager : MonoBehaviour
{
    public GameManager gm;
    public UIController UI;
    [SerializeField]
    private Spawner pidgeon;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        gm.UI=UI;
        pidgeon.enable();
    }


}
