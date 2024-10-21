using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernManager : MonoBehaviour
{
    [SerializeField] private GameObject textbox, textbox2;
    [SerializeField] private GameObject tv, sockScene;
    [SerializeField] private Animator transition;
    bool isEndConvo = false;
    bool isEndConvo2 = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayDialogue());
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(1.5f);
        textbox.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        isEndConvo = true;
    }

    IEnumerator DelayDialogue2()
    {
        isEndConvo = false;
        tv.SetActive(true);

        yield return new WaitForSeconds(0.15f);
        sockScene.SetActive(true);

        yield return new WaitForSeconds(1f);
        textbox2.SetActive(true);

        yield return new WaitForSeconds(1f);
        isEndConvo2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEndConvo && !textbox.activeSelf)
        {
            StartCoroutine(DelayDialogue2());
        }

        if(isEndConvo2 && !textbox2.activeSelf)
        {
            StartCoroutine(DelayEnd());
        }
    }

    IEnumerator DelayEnd()
    {
        transition.SetBool("end scene", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
