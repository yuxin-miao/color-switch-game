using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
  public TMP_Text scoreText;
  private int lastScore = 0;

  void Update()
  {
    if (lastScore != GameManager.Instance.collectedPoint)
    {
      lastScore = GameManager.Instance.collectedPoint;
      scoreText.text = lastScore.ToString();
    }

  }
}
