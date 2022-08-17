using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
  public Camera cam;
  public Transform subject;

  float startZ;
  float startX;

  float travel => cam.transform.position.x - startX;

  float distanceFromSubject => transform.position.z - subject.position.z;

  float parallaxFactor => 10f / distanceFromSubject;

  private float offset;
  private Material mat;
  // Start is called before the first frame update
  void Start()
  {
    mat = GetComponent<Renderer>().material;
    startX = transform.position.x;
    startZ = transform.position.z;
    offset = 0;
  }

  // Update is called once per frame
  void Update()
  {
    offset = Mathf.Abs(travel * parallaxFactor);
    mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
  }
}
