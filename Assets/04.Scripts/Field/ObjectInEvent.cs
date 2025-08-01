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
            Debug.Log("µé¾î¿È");
            SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
            sr.sortingOrder += 500;
            Material mat = sr.material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.75f);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("³ª°¨");
            SpriteRenderer sr = tree.GetComponent<SpriteRenderer>();
            sr.sortingOrder -= 500;
            Material mat = sr.material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);

        }
    }

}
