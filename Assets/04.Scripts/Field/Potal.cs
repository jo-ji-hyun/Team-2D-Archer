using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("디버그확인1");

        if (other.CompareTag("Player"))
        {
            Debug.Log("디버그확인2");
        }
    }
}

