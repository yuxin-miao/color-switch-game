using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
  // Handle player move and interact with items 

  public float jumpForce = 5f;
  public GameObject playerDeath; // Particle system reference 
  public GameObject starCollection;// Particle system reference 

  private Rigidbody2D rb2D; 
  private SpriteRenderer ballRenderer;
  private int currentColorIndex = 0; // Store current color index for color switching during runtime 
  private SoundManager soundManager;

  void Start()
  {
    soundManager = FindObjectOfType<SoundManager>();

    rb2D = GetComponent<Rigidbody2D>();
    ballRenderer = GetComponent<SpriteRenderer>();
    SetRandomColor(); // Randomly selected one color from the predetermined four colors, and set as ball color 

  }

  void Update()
  {
    // Only update when game is running 
    if (!GameManager.Instance.isGameOver)
    {
      // If player fall out of the screen, game over
      float cameraBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
      if (transform.position.y < cameraBottomY)
      {
        TriggerGameOver();
      }

      // Handle player input, jump when press anywhere 
      if (Input.GetMouseButtonDown(0))
      {
        Jump();
      }
    }
  }

  // Handle player interact with different items 
  private void OnTriggerEnter2D(Collider2D collision)
  {
    // Interact with an obstacle with different color, game over 
    if (collision.gameObject.CompareTag("Arc") && ballRenderer.color != collision.gameObject.GetComponent<SpriteRenderer>().color)
    {
      TriggerGameOver();
    }

    // Interact with color switcher to change the color of the ball 
    if (collision.gameObject.CompareTag("ColorSwitcher"))
    {
      SwitchColor(collision);
    }

    // Interact with the Star to collect point 
    if (collision.gameObject.CompareTag("Point"))
    {
      CollectPoint(collision);
    }

    // Interact with the Finish line 
    if (collision.gameObject.CompareTag("Finish"))
    {
      TriggerGameFinish();
    }

    // TODO: change to Switch, instead of mutiple IF statements. Better for adding more types of interactable items 
  }

  void Jump()
  {
    rb2D.velocity = Vector2.up * jumpForce;
    soundManager.jumpSound.Play();
  }
  void TriggerGameOver()
  {
    if (GameManager.Instance.testMode) return; // Player won't die under teseMode 
    if (transform)
    {
      // Spawn a particle system to indicate the ball being destroyed 
      GameObject particleInstance = Instantiate(playerDeath, transform.position, Quaternion.identity);
      ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
      ps.Play();
      soundManager.defeatSound.Play();
      gameObject.SetActive(false);  

      // Let GameManager handle game state 
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
