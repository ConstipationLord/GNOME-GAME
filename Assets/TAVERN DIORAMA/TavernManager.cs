using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernManager : MonoBehaviour
{
    [SerializeField] private GameObject textbox;
    [SerializeField] private Animator transition;
    bool isEndConvo = false;

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

    // Update is called once per frame
    void Update()
    {
        if (isEndConvo && !textbox.activeSelf)
        {
            transition.SetBool("end scene", true);
            StartCoroutine(DelayEnd());
        }
    }

    IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
