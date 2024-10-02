using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject textbox;
    [SerializeField] private Animator transition;
    bool isEndConvo = false;

    [SerializeField] private GameObject batAnim;
    [SerializeField] private GameObject gnomeObj;

    bool check;

    // Start is called before the first frame update
    void Start()
    {
        check = false;
        StartCoroutine(DelayDialogue());
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(13f);
        isEndConvo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEndConvo && !textbox.activeSelf &&!check)
        {
            check = true;
            batAnim.SetActive(true);
            StartCoroutine(DelayPickGnomeUp());
        }
    }

    IEnumerator DelayPickGnomeUp()
    {
        yield return new WaitForSeconds(1f);
        gnomeObj.transform.position = new Vector3(59.0999985f, 201.300003f, 102.919998f);
        gnomeObj.transform.eulerAngles = new Vector3(0, 285.01001f, 0);

        yield return new WaitForSeconds(0.5f);
        gnomeObj.transform.SetParent(batAnim.transform);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3);
    }
}
