using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilegridPopulator : MonoBehaviour
{
    #region Serialized Fields

    [Header("Enemies")]

    [SerializeField]
    [Range(0, 1)]
    private float chanceOfEnemies = 0.5f;

    [SerializeField]
    private float enemySpawnRate = 5f;

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    [Header("Obstacles")]

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Note: Obstacles will not spawn on layers with enemies.")]
    private float chanceOfObstacles = 0.5f;

    [SerializeField]
    private float obstacleDensity = 3;

    [SerializeField]
    private List<GameObject> obstaclePrefabs;

    [Header("Foliage")]

    [SerializeField]
    [Range(0, 1)]
    private float chanceOfFoliage = 0.8f;

    [SerializeField]
    private float foliageDensity = 10f;

    [SerializeField]
    private List<GameObject> foliagePrefabs;

    #endregion

    #region Properties

    private int? enemyPrefabIndex;

    private DirectionEnum enemyRunDirection;

    private HashSet<Vector2Int> populatedSpaces = new HashSet<Vector2Int>();

    private bool hasObstacles = false;

    public bool IsSafeRow = false;

    #endregion

    #region Fields

    private bool IsEmpty => !this.HasEnemies && !this.hasObstacles;

    private bool HasEnemies => this.enemyPrefabIndex.HasValue;

    private GameObject EnemyPrefab => this.HasEnemies
        ? this.enemyPrefabs[this.enemyPrefabIndex.Value]
        : throw new System.Exception("Attempting to spawn enemies in a non-enemy tilegrid.");

    #endregion

    #region Overrides

    private void Start()
    {
        if (!this.IsSafeRow && Random.value < this.chanceOfEnemies)
        {
            this.StartCoroutine(this.StartSpawnLoop());
        }
        else if (Random.value < this.chanceOfObstacles)
        {
            this.SpawnObstacles();
        }

        if (!this.hasObstacles && Random.value < this.chanceOfFoliage)
        {
            this.SpawnFoliage();
        }
    }

    #endregion

    #region Helpers

    private void SpawnObstacles()
    {
        this.hasObstacles = true;
        this.SpawnRandomObjects(this.obstacleDensity, this.obstaclePrefabs, true);
    }

    private void SpawnFoliage()
        => this.SpawnRandomObjects(this.foliageDensity, this.foliagePrefabs);

    private void SpawnRandomObjects(float density, List<GameObject> prefabs, bool snapToGrid = false)
    {
        int objectCount = Random.Range(1, Mathf.RoundToInt(density));

        for (int i = 0; i < objectCount; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Count - 1)];
            ShapeAndPositionInfo shapeAndPosition = prefab.GetComponent<ShapeAndPositionInfo>();
            Vector2? spawnPoint = this.GetRandomOpenSpace(shapeAndPosition, snapToGrid);

            if (spawnPoint.HasValue)
            {
                Instantiate(prefab, (Vector2)this.transform.position + spawnPoint.Value, Quaternion.identity, this.transform);
                this.MarkSpacesAsFilled(shapeAndPosition, new Vector2Int((int)spawnPoint.Value.x, (int)spawnPoint.Value.y));
            }
        }
    }

    private Vector2? GetRandomOpenSpace(ShapeAndPositionInfo shapeAndPosition, bool snapToGrid = false)
    {
        // Limit how many tries we make, in case we get stuck in an impossible loop.
        for (int attempt = 0; attempt < 100; attempt++)
        {
            bool success = true;
            int xVal = snapToGrid ? Random.Range(-5, 5) * 3 : Random.Range(-15, 15);
            int yVal = Random.Range(-shapeAndPosition.OffsetVariance.y, shapeAndPosition.OffsetVariance.y + 1);

            int xOffsetStart = shapeAndPosition.PositionOffset.x;
            int xOffsetEnd = xOffsetStart + shapeAndPosition.Shape.x;
            int yOffsetStart = shapeAndPosition.PositionOffset.y;
            int yOffsetEnd = yOffsetStart + shapeAndPosition.Shape.y;

            // Check if there's enough room for the entire shape.
            for (int xOffset = xOffsetStart; xOffset < xOffsetEnd; xOffset++)
            {
                for (int yOffset = yOffsetStart; yOffset < yOffsetEnd; yOffset++)
                {
                    if (this.populatedSpaces.Contains(new Vector2Int(xVal + xOffset, yVal + shapeAndPosition.PositionOffset.y)))
                    {
                        // Not enough room, try again.
                        success = false;
                        break;
                    }
                }

                // Check if we should fail out early.
                if (!success)
                {
                    break;
                }
            }

            if (success)
            {
                float xPos = xVal + shapeAndPosition.PositionOffset.x;
                float yPos = yVal + shapeAndPosition.PositionOffset.y;
                return new Vector2(xPos, yPos);
            }
        }

        // The only way to get here is if we failed to find an open space 100 times.
        // This is very unlikely, but we should still handle it elegantly.
        return null;
    }

    private void MarkSpacesAsFilled(ShapeAndPositionInfo shapeAndPosition, Vector2Int startPoint)
    {
        for (int xOffset = 0; xOffset < shapeAndPosition.Shape.x; xOffset++)
        {
            for (int yOffset = 0; yOffset < shapeAndPosition.Shape.y; yOffset++)
            {
                this.populatedSpaces.Add(new Vector2Int(startPoint.x + xOffset, startPoint.y + yOffset));
            }
        }
    }

    #endregion

    #region Coroutines

    private IEnumerator StartSpawnLoop()
    {
        // Set up enemy info.
        this.enemyPrefabIndex = (int)(Random.value * this.enemyPrefabs.Count);
        this.enemyRunDirection = Random.value < 0.5f ? DirectionEnum.Left : DirectionEnum.Right;

        // Wait a random amount of time before spawning the first enemy.
        yield return new WaitForSeconds(Random.value * this.enemySpawnRate);
        this.StartCoroutine(this.SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        Vector2 spawnPoint = this.transform.position + new Vector3(18 * -this.enemyRunDirection.GetXSign(), 0);
        GameObject spawnedEnemy = Instantiate(this.EnemyPrefab, spawnPoint, Quaternion.identity, this.transform);
        spawnedEnemy.GetComponent<NpcMovement>().RunDirection = this.enemyRunDirection;
        yield return new WaitForSeconds(this.enemySpawnRate);
        this.StartCoroutine(this.SpawnLoop());
    }

    #endregion
}
