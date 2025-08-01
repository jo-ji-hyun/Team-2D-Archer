using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject UI2;
    public GameObject UI3;
    public GameObject UI4;

    public GameObject MainCamera;
    public GameObject TitleCamera;

    public GameObject Player;

    public Animator Tilte;

    private void Start()
    {
        UI.SetActive(true);
        StartCoroutine(UIOffFade());
    }

    IEnumerator UIOffFade()
    {
        yield return new WaitForSeconds(2f);
        UI.SetActive(false);
    }

    public void ButtonOff()
    {
        StartCoroutine(UIOffButton());
        Tilte.SetBool("Start", true);
    }

    IEnumerator UIOffButton()
    {

        UI2.SetActive(false);
        UI3.SetActive(false);

        yield return new WaitForSeconds(3f);

        UI4.SetActive(false);

        yield return new WaitForSeconds(1f);

        MainCamera.SetActive(true);
        TitleCamera.SetActive(false);

    }
}
