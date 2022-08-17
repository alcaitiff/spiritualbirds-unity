using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
  public float smoothness;
  public Transform targetObject;
  private float initalOffsetX;
  private Vector3 cameraPosition;
  private float cameraOffset = 4;

  void Start()
  {
    initalOffsetX = transform.position.x - targetObject.position.x;
  }

  void Update()
  {
    cameraPosition = new Vector3(targetObject.position.x + initalOffsetX, transform.position.y, transform.position.z);
    if (Mathf.Abs(Mathf.Abs(cameraPosition.x) - Mathf.Abs(transform.position.x)) < cameraOffset)
    {
      float delta = (cameraPosition.x - transform.position.x) * 0.02f;
      cameraPosition = new Vector3(transform.position.x + delta + initalOffsetX, transform.position.y, transform.position.z);
    }
    transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
  }
}


