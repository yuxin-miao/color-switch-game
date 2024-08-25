using UnityEngine;

public class Scaler : MonoBehaviour
{
  public float scaleSpeed = 1.0f; 
  public float scaleAmount = 0.5f; 

  private Vector3 originalScale; 

  void Start()
  {
    originalScale = transform.localScale;
  }

  void Update()
  {
    float scale = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount + 1; 
    transform.localScale = originalScale * scale;
  }
}
