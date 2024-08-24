using UnityEngine;

public class ObstacleController: MonoBehaviour
{
  public bool rotate = true;
  public bool changeColor = false;
  public bool move = false;

  public float rotationSpeed = 90.0f;
  public float moveSpeed = 3.0f;

  private float moveDistance = 4;
  private Vector3 startPosition;
  private SpriteRenderer[] childRenderers;
  private int startIndex = 0;
  private void Start()
  {
    childRenderers = GetComponentsInChildren<SpriteRenderer>();

    AssignColor();
    startPosition = transform.position;
  }
  void Update()
  {
    if (rotate)
    {
      Quaternion rot = Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
      transform.rotation = transform.rotation * rot;
    }
    if (move)
    {
      float movement = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
      transform.position = startPosition + Vector3.right * movement;
    }

    if (Input.GetMouseButtonDown(0) && changeColor)
    {
      startIndex++;
      AssignColor();
    }


  }
  private void AssignColor()
  {
    if (childRenderers.Length == 1)
    {
      childRenderers[0].color = GameManager.Instance.GetColor(Random.Range(0, GameManager.Instance.colors.Count));
    }
    else
    {
      foreach (SpriteRenderer child in childRenderers)
      {
        child.color = GameManager.Instance.GetColor(startIndex);
        startIndex++;
      }
    }

  }
}
