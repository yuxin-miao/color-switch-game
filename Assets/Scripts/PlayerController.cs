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
  private SoundManager soundManager;

  void Start()
  {
    soundManager = FindObjectOfType<SoundManager>();

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
      SwitchColor(collision);
    }
    if (collision.gameObject.CompareTag("Point"))
    {
      CollectPoint(collision);
    }
    if (collision.gameObject.CompareTag("Finish"))
    {
      TriggerGameFinish();
    }
  }

  void Jump()
  {
    rb2D.velocity = Vector2.up * jumpForce;
    soundManager.jumpSound.Play();
  }
  void TriggerGameOver()
  {
    if (GameManager.Instance.testMode) return;
    if (transform)
    {
      GameObject particleInstance = Instantiate(playerDeath, transform.position, Quaternion.identity);
      ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
      ps.Play();
      soundManager.defeatSound.Play();
      gameObject.SetActive(false);  
      GameManager.Instance.GameOver();
    }
  }
  void TriggerGameFinish()
  {
    if (transform)
    {
      GameObject particleInstance = Instantiate(starCollection, transform.position, Quaternion.identity);
      ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
      ps.Play();
      soundManager.collectSound.Play();
      gameObject.SetActive(false);
      GameManager.Instance.GameFinish();
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

  void SwitchColor(Collider2D collision)
  {
    int colorCount = GameManager.Instance.colors.Count;
    currentColorIndex = (currentColorIndex + 1) % colorCount;
    ballRenderer.color = GameManager.Instance.GetColor(currentColorIndex);
    soundManager.switchSound.Play();
    GameManager.Instance.activeObstaclesCount--;
    Destroy(collision.gameObject);

  }
  void CollectPoint(Collider2D collision)
  {
    GameObject particleInstance = Instantiate(starCollection, collision.transform.position, Quaternion.identity);
    ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
    ps.Play();
    GameManager.Instance.collectedPoint++;
    GameManager.Instance.activeObstaclesCount--;
    soundManager.collectSound.Play();
    Destroy(collision.gameObject);

  }

}
