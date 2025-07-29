using System.Collections;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;

    private int height = 10;
    private int width = 10;

    private float tileWidth = 1.9204f;
    private float tileHeight = -1.108f;

    void Start()
    {
        StartCoroutine(SpawnTiles());
    }

    IEnumerator SpawnTiles()
    {
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y + 4.69f, transform.position.z);
        int i = width * height;

        for (int y = width - 1; y >= 0; y--)
        {
            for (int x = height - 1; x >= 0; x--)
            {
                float xPos = (x - y) * tileWidth / 2f;
                float yPos = (x + y) * tileHeight / 2f;

                Vector3 spawnPos = startPos + new Vector3(xPos, yPos, 0f);

                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);

                SpriteRenderer sr = tile.GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingOrder = i;
                }
                i--;
            }

            // 한 줄 완성 후 딜레이
            yield return new WaitForSeconds(0.1f);
        }
    }
}

