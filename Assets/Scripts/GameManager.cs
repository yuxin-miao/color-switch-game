using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;  

  public GameObject gameOverScreen;    
  public bool isGameOver = false;  

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
  }

  void Update()
  {
    if (isGameOver && Input.GetMouseButtonDown(0))
    {
      RestartGame();
    }
  }

  public void GameOver()
  {
    isGameOver = true; 
    if (gameOverScreen != null)
    {
      gameOverScreen.SetActive(true);  
    }
  }

  public void RestartGame()
  {
    isGameOver = false;  
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
  }
}
