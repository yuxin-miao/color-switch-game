using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
  public TMP_Text scoreText;
  public GameObject finishText;
  public Toggle hardToggle;
  public ObstacleSpawner obstacleSpawner;
  private int lastScore = 0;

  void Update()
  {
    if (lastScore != GameManager.Instance.collectedPoint)
    {
      lastScore = GameManager.Instance.collectedPoint;
      scoreText.text = lastScore.ToString();
    }
    if (GameManager.Instance.isEndless)
    {
      finishText.SetActive(false);
      hardToggle.interactable = true;
    } else
    {
      if (!finishText.activeSelf)
      {
        finishText = Instantiate(finishText, new Vector3(0, obstacleSpawner.lastObstacle.transform.position.y + 5.0f, 0), Quaternion.identity);
        finishText.SetActive(true);
      }

      hardToggle.interactable = false;
    }
  }
  public void onClickEndless()
  {
    GameManager.Instance.isEndless = !GameManager.Instance.isEndless;
  }
  public void onClickHard()
  {
    GameManager.Instance.isHard = !GameManager.Instance.isHard;
  }

  public void onClickTest()
  {
    GameManager.Instance.testMode = !GameManager.Instance.testMode;
  }
}
