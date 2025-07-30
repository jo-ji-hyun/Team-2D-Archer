using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject[] objectPrefab;
    public Sprite[] tileSprites;

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

        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = width - 1; x >= 0; x--)
            {
                float xPos = (x - y) * tileWidth / 2.06f;
                float yPos = (x + y) * tileHeight / 2.06f;

                Vector3 spawnPos = startPos + new Vector3(xPos, yPos, 0f);

                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);

                SpriteRenderer sr = tile.GetComponentInChildren<SpriteRenderer>();
                sr.sortingOrder = i;

                if (UnityEngine.Random.Range(0, 100) <= 10)
                {
                    sr.sprite = tileSprites[1];
                }

                if (UnityEngine.Random.Range(0, 100) <= 10)
                {
                    spawnPos = startPos + new Vector3(xPos + UnityEngine.Random.Range(-0.3f, 0.3f), yPos + UnityEngine.Random.Range(0.2f, 1f), 0f);
                    GameObject _object = Instantiate(objectPrefab[UnityEngine.Random.Range(1, 4)], spawnPos, Quaternion.identity, transform);
                    sr = _object.GetComponentInChildren<SpriteRenderer>();
                    sr.sortingOrder = i + 20;
                }

                if (UnityEngine.Random.Range(0, 100) <= 2)
                {
                    spawnPos = startPos + new Vector3(xPos + UnityEngine.Random.Range(-0.3f, 0.3f), yPos + UnityEngine.Random.Range(0.2f, 1f), 0f);
                    GameObject _object = Instantiate(objectPrefab[UnityEngine.Random.Range(0, 1)], spawnPos, Quaternion.identity, transform);
                    sr = _object.GetComponentInChildren<SpriteRenderer>();
                    sr.sortingOrder = i + 20;
                }
                i--;
            }

            // ÇÑÁÙ µô·¹ÀÌ
            yield return new WaitForSeconds(0.1f);
        }

        for (int y = 0; y >= -3; y--)
        {
            for (int x = width - 1; x >= 0; x--)
            {
                if (x >= 4 && x< 6)
                {
                    float xPos = (x - y) * tileWidth / 2.06f;
                    float yPos = (x + y) * tileHeight / 2.06f;

                    Vector3 spawnPos = startPos + new Vector3(xPos, yPos, 0f);

                    GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);

                    SpriteRenderer sr = tile.GetComponentInChildren<SpriteRenderer>();
                    sr.sortingOrder = i;
                    i--;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }


    }
}

