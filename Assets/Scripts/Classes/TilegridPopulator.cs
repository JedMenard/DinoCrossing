using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilegridPopulator : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    [SerializeField]
    private List<GameObject> obstaclePrefabs;

    [SerializeField]
    [Range(0, 1)]
    private float chanceOfEmptyRow = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    private float chanceOfEnemies = 0.5f;

    [SerializeField]
    private float EnemySpawnRate = 5f;

    #endregion

    #region Properties

    private int? enemyPrefabIndex;

    private int? obstaclePrefabIndex;

    #endregion

    #region Fields

    private bool IsEmpty => !this.HasEnemies && !this.HasObstacles;

    private bool HasEnemies => this.enemyPrefabIndex.HasValue;

    private bool HasObstacles => this.obstaclePrefabIndex.HasValue;

    private GameObject EnemyPrefab => this.HasEnemies
        ? this.enemyPrefabs[this.enemyPrefabIndex.Value]
        : throw new System.Exception("Attempting to spawn enemies in a non-enemy tilegrid.");

    private GameObject ObstaclePrefab => this.HasObstacles
        ? this.obstaclePrefabs[this.obstaclePrefabIndex.Value]
        : throw new System.Exception("Attempting to spawn obstacles in a non-obstacle tilegrid.");

    #endregion

    #region Overrides

    private void Start()
    {
        if (Random.value < this.chanceOfEmptyRow)
        {
            this.enabled = false;
            return;
        }
        else if (Random.value < this.chanceOfEnemies)
        {
            this.enemyPrefabIndex = (int)(Random.value * this.enemyPrefabs.Count);
            this.StartCoroutine(this.StartSpawnLoop());
        }
        else
        {
            this.obstaclePrefabIndex = (int)(Random.value * this.obstaclePrefabs.Count);
        }
    }

    #endregion

    #region Helpers

    private IEnumerator StartSpawnLoop()
    {
        // Wait a random amount of time before spawning the first enemy.
        yield return new WaitForSeconds(Random.value * this.EnemySpawnRate);
        this.StartCoroutine(this.SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        Vector2 spawnPoint = this.transform.position + new Vector3(18, 1);
        Instantiate(this.EnemyPrefab, spawnPoint, Quaternion.identity, this.transform);
        yield return new WaitForSeconds(this.EnemySpawnRate);
        this.StartCoroutine(this.SpawnLoop());
    }

    #endregion
}
