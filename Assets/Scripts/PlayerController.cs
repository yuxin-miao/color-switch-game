using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

  public float jumpForce = 5f; 
  public GameObject playerDeath;
  public GameObject starCollection;

  private Rigidbody2D rb2D;
  private SpriteRenderer ballRenderer;
  private int currentColorIndex = 0;
  
  void Start()
  {
    rb2D = GetComponent<Rigidbody2D>();
    ballRenderer = GetComponent<SpriteRenderer>();
    SetRandomColor();
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
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Arc") && ballRenderer.color != collision.gameObject.GetComponent<SpriteRenderer>().color)
    {
      TriggerGameOver();
    }
    if (collision.gameObject.CompareTag("ColorSwitcher"))
    {
      SwitchColor();
      Destroy(collision.gameObject);
    }
    if (collision.gameObject.CompareTag("Point"))
    {
      GameObject particleInstance = Instantiate(starCollection, collision.transform.position, Quaternion.identity);
      ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
      ps.Play();
      GameManager.Instance.collectedPoint++;
      Destroy(collision.gameObject);
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
      GameObject particleInstance = Instantiate(playerDeath, transform.position, Quaternion.identity);
      ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
      ps.Play();

      gameObject.SetActive(false);  
      GameManager.Instance.GameOver();
    }
  }

  void SetRandomColor()
  {
    if (GameManager.Instance.colors.Count > 0)
    {
      int colorIndex = Random.Range(0, GameManager.Instance.colors.Count);
      currentColorIndex = colorIndex;
      ballRenderer.color = GameManager.Instance.GetColor(colorIndex);
    }
  }

  void SwitchColor()
  {
    int colorCount = GameManager.Instance.colors.Count;
    currentColorIndex = (currentColorIndex + 1) % colorCount;
    ballRenderer.color = GameManager.Instance.GetColor(currentColorIndex);
  }

}
