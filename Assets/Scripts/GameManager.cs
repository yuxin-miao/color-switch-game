using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;  

  public GameObject gameOverScreen;    
  public bool isGameOver = false;

  public List<Color> colors = new List<Color>();
  public int collectedPoint = 0;


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

  public Color GetColor(int index)
  {
    if (index >= 0 && index < colors.Count)
    {
      return colors[index];
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

  public void RestartGame()
  {
    isGameOver = false;  
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
  }
}
