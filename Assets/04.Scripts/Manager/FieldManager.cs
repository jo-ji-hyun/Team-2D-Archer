using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance;
    public GameObject RoomPrefab;
    public Transform Player;
    public Animator UI;

    public int MonsterCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UIFade()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        UI.SetInteger("Fade", 1);

        yield return new WaitForSeconds(1f);

        UI.SetInteger("Fade", 2);

        yield return new WaitForSeconds(1f);

        UI.SetInteger("Fade", 3);

        GameObject room = GameObject.Find("Room");
        Destroy(room);

        Player.transform.position = new Vector3(-3.7f, -1.4f, 0f);
        Instantiate(RoomPrefab, new Vector3(0, 0, 0), Quaternion.identity).name = "Room";

        yield return new WaitForSeconds(1f);

        UI.SetInteger("Fade", 0);

        GameManager.Instance.StartWave();
    }

}
