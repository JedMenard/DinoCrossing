using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private int maxTileCount = 10;

    [SerializeField]
    private List<GameObject> prefabs;

    #endregion

    #region Components

    #endregion

    #region Properties

    private Vector2 spawnPoint = new Vector2(0, 0);

    private Queue<GameObject> tileQueue = new Queue<GameObject>();

    #endregion

    #region Fields

    #endregion

    #region Overrides

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            this.SpawnTiles();
        }
    }

    #endregion

    #region Helpers

    public void SpawnTiles()
    {
        GameObject prefab = this.prefabs[Random.Range(0, this.prefabs.Count)];
        this.tileQueue.Enqueue(Instantiate(prefab, this.spawnPoint, Quaternion.identity, this.transform));

        this.spawnPoint += new Vector2(0, 3);
        FindObjectOfType<CinemachineConfiner>().InvalidatePathCache();

        if (this.tileQueue.Count > this.maxTileCount)
        {
            Destroy(this.tileQueue.Dequeue());
        }
    }

    #endregion
}
