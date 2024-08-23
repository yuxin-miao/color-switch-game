using UnityEngine;

public class Rotator : MonoBehaviour
{
  public float rotationSpeed = 90.0f;
  private void Start()
  {
    AssignColor();
  }
  void Update()
  {
    Quaternion rot = Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
    transform.rotation = transform.rotation * rot;
  }
  private void AssignColor()
  {
    SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
    int index = 0;
    foreach (SpriteRenderer child in childRenderers)
    {
      child.color = GameManager.Instance.GetColor(index);
      index++;
    }
  }
}
