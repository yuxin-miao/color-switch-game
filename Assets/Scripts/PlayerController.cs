using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
  public float jumpForce = 5f; 
  private Rigidbody2D rb2D;
  public ParticleSystem playerDeath;

  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
  }

  void Update()
  {

    if (!GameManager.Instance.isGameOver)
    {
      float cameraBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

      if (transform.position.y < cameraBottomY)
      {
        TriggerGameOver();
      }

      if (Input.GetMouseButtonDown(0))
      {
        Jump();
      }
    }
  }

  void Jump()
  {
    rb2D.velocity = Vector2.up * jumpForce;
  }
  void TriggerGameOver()
  {
    if (transform)
    {
      playerDeath.transform.position = transform.position;
      playerDeath.Play();
      gameObject.SetActive(false);  
      GameManager.Instance.GameOver();
    }
  }
}
