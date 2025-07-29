using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;

    private int height = 20;
    private int width = 20;

    private float tileWidth = 0.9602f;
    private float tileHeight = -0.554f;

    void Start()
    {
        SpawnTiles();
    }

    void SpawnTiles()
    {
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y + 5.25f, transform.position.z);

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                float xPos = (x - y) * tileWidth / 2f;
                float yPos = (x + y) * tileHeight / 2f;

                Vector3 spawnPos = startPos + new Vector3(xPos, yPos, 0f);
                Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }
}

