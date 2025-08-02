using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInEvent : MonoBehaviour
{

    [SerializeField]
    private GameObject tree;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("들어옴");
            SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
            sr.sortingOrder += 500;
            Material mat = sr.material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.75f);

        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("몬스터 들어감");
            SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
            sr.sortingOrder += 500;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("나감");
            SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
            sr.sortingOrder -= 500;
            Material mat = sr.material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);

        }

        if (other.CompareTag("Enemy"))
        {

            Debug.Log("나감");
            SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
            sr.sortingOrder -= 500;

        }
    }

}
