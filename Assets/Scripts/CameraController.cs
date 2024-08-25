using UnityEngine;

public class CameraController : MonoBehaviour
{
  // Make the camera follow player in the positive y-axis only

  public Transform ball;             
  private float damping = 0.5f;
  private Vector3 vel = Vector3.zero;
  private void FixedUpdate()
  { 
    if (ball.position.y > transform.position.y)
    {
      // Smoothly move towards the target 
      Vector3 targetPosition = new Vector3(transform.position.x, ball.position.y, transform.position.z);
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }

  }
}
