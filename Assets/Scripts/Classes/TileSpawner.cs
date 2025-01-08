using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private int safeStartTiles = 5;

    [SerializeField]
    private int maxTileCount = 10;

    [SerializeField]
    private List<GameObject> prefabs;

    #endregion

    #region Components

    #endregion

    #region Properties

    private Vector2 spawnPoint = new Vector2(0, -11);

    private Queue<GameObject> tileQueue = new Queue<GameObject>();

    #endregion

    #region Fields

    #endregion

    #region Overrides

    private void Start()
    {
        for (int i = 0; i < this.maxTileCount; i++)
        {
            this.SpawnTiles(i < this.safeStartTiles);
        }
    }

    #endregion

    #region Helpers

    public void SpawnTiles(bool disablePopulator = false)
    {
        GameObject prefab = this.prefabs[Random.Range(0, this.prefabs.Count)];
        GameObject spawnedTiles = Instantiate(prefab, this.spawnPoint, Quaternion.identity, this.transform);
        this.tileQueue.Enqueue(spawnedTiles);

        if (disablePopulator && spawnedTiles.TryGetComponent(out TilegridPopulator populator))
        {
            populator.enabled = false;
        }

        this.spawnPoint += new Vector2(0, 3);
        FindObjectOfType<CinemachineConfiner>().InvalidatePathCache();

        if (this.tileQueue.Count > this.maxTileCount)
        {
            Destroy(this.tileQueue.Dequeue());
        }
    }

    #endregion
}
