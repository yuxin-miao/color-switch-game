using UnityEngine;
using System.Collections.Generic;
public class ObstacleSpawner : MonoBehaviour
{
  // Automatic spawn obstacles if user enables the endless mode 

  public List<GameObject> obstaclePrefabs = new List<GameObject>();
  public List<GameObject> hardObstaclePrefabs = new List<GameObject>(); // only spawn if player like challenging 

  public GameObject colorSwitcherPrefab;
  public GameObject starPrefab;
  public GameObject lastObstacle;

  // TODO: based on the obstacle, select different spawn distance 
  public float spawnDistance = 7.0f;

  void Update()
  {
    if (GameManager.Instance.isEndless)
    {
      SpawnObstacles();
    }
  }

  void SpawnObstacles()
  {
    if (GameManager.Instance.activeObstaclesCount < 5)
    {
      int spawnCount = 10 - GameManager.Instance.activeObstaclesCount; // The number of obstacles existed won't exceed a certain number 

      for (int i = 0; i < spawnCount; i++)
      {
        // Spawn position is determined by last obstacle, which is updated every time when spawn a new obstacle 
        Vector3 spawnPosition = new Vector3(0, lastObstacle.transform.position.y + spawnDistance, 0);

        lastObstacle = Instantiate(selectNextObstacle(), spawnPosition, Quaternion.identity);
        
        GameManager.Instance.activeObstaclesCount++;
      }

    }
  }

  GameObject selectNextObstacle()
  {

    // Select a prefab, randomly choose between obstacles, star, and color switcher 
    // TODO: based on user preference to select different obstacles
    float randomValue = Random.value;

    if (randomValue > 0.6f && lastObstacle.gameObject.tag != "Point") {
      return starPrefab;
    }

    if (randomValue > 0.4f && lastObstacle.gameObject.tag != "ColorSwitcher")
    {
      return colorSwitcherPrefab;
    }

    if (randomValue < 0.5f)
    {
      if (GameManager.Instance.isHard && randomValue < 0.1f)
      {
        return hardObstaclePrefabs[Random.Range(0, hardObstaclePrefabs.Count)];
      }
      else return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
    }

    return obstaclePrefabs[0];
  }
}
