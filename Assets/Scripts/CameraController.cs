using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform ball;             
  private float damping = 0.5f;
  private Vector3 vel = Vector3.zero;
  private void FixedUpdate()
  {
    if (ball.position.y > transform.position.y)
    {
      Vector3 targetPosition = new Vector3(transform.position.x, ball.position.y, transform.position.z);
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }

  }
}
