using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�����Ȯ��1");

        if (other.CompareTag("Player"))
        {
            Debug.Log("�����Ȯ��2");
        }
    }
}

