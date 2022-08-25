using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BackgroundTimedScroller : MonoBehaviour
{
    [Range(-1f,1f)]
    public float scrollSpeed = 0.5f;
    public bool stop = false;
    private float offset;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Math.Abs(scrollSpeed)>0.01f){
            if(stop){
               scrollSpeed*=0.999f;
            }
            offset += (Time.deltaTime * scrollSpeed) / 10f;
            mat.SetTextureOffset("_MainTex", new Vector2(offset,0));
        }
    }

}
