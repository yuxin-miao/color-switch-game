using UnityEngine;

public class ObstacleController: MonoBehaviour
{
  // Control the obstacle move, rotate, and color change 
  public bool rotate = true;
  public bool changeColor = false;
  public bool move = false;

  public float rotationSpeed = 90.0f;
  public float moveSpeed = 3.0f;

  private float moveDistance = 2.0f;
  private Vector3 startPosition;
  private SpriteRenderer[] childRenderers;
  private int startIndex = 0;
  private PlayerController player;
  private void Start()
  {
    childRenderers = GetComponentsInChildren<SpriteRenderer>();
    player = FindObjectOfType<PlayerController>();
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
      float movement = Mathf.PingPong(Time.time * moveSpeed, 2 * moveDistance) - moveDistance;
      transform.position = startPosition + Vector3.right * movement;
    }

    // When user click anywhere, the obstacle will change color following the order
    if (Input.GetMouseButtonDown(0) && changeColor)
    {
      startIndex++;
      AssignColor();
    }

    // Destroy the obstacle if it is out of the screen
    if (player != null && transform.position.y < (player.transform.position.y - 6.0f))
    {
      GameManager.Instance.activeObstaclesCount--;
      Destroy(gameObject);
    }
  }
  private void AssignColor()
  {
    // The color is assigned after game start, so the obstacles color will always match the color determined in GameManager 
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
