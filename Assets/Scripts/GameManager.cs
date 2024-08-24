using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;  

  public GameObject gameOverScreen;
  public GameObject gameFinishScreen;
  public bool isGameOver = false;

  public List<Color> colors = new List<Color>();
  public int collectedPoint = 0;
  public float timer; 

  public bool isEndless = false;
  public bool isHard = false;
  public bool testMode = false;

  public int activeObstaclesCount = 12;
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
    timer += Time.deltaTime;
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
