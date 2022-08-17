using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScroller : MonoBehaviour
{

    public float factor = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x+Time.deltaTime*factor,transform.position.y,transform.position.z);
    }
}
