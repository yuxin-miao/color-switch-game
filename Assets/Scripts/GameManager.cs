using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance; // For easy access

  public GameObject gameOverScreen;
  public GameObject gameFinishScreen;
  public bool isGameOver = false;

  // All other comoponents will get color from gameManager. Easy to change color for the whole game 
  // TODO: Allow user to pick up colors and change  
  public List<Color> colors = new List<Color>(); // Predetermined colors 
  public int collectedPoint = 0;

  // Add differnt mode for various gaming experience 
  public bool isEndless = false;
  public bool isHard = false;
  public bool testMode = false;

  public int activeObstaclesCount = 12;
  void Awake()
  {
    // Only one gameManager instance exist 
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
    // Handle restart when user tap anywhere 
    if (isGameOver && Input.GetMouseButtonDown(0))
    {
      RestartGame();
    }
  }

  public Color GetColor(int index)
  {
    if (index >= 0)
    {
      return colors[index % (colors.Count)];
    }
    return Color.white;
  }

  public void GameOver()
  {
    isGameOver = true; 
    if (gameOverScreen != null)
    {
      gameOverScreen.SetActive(true);  
    }
  }
  public void GameFinish()
  {
    isGameOver = true;
    if (gameFinishScreen != null)
    {
      gameFinishScreen.SetActive(true);
    }
  }
  public void RestartGame()
  {
    isGameOver = false;  
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
  }
}
