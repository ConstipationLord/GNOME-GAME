using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernOpen : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;

    [SerializeField] private GameObject transi;

    bool isInside = false;
    bool check = false;

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.Find("BAT 1") == null && GameObject.Find("BAT 2") == null && GameObject.Find("BAT 3") == null)
        {
            isInside = true;
            check = true;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(DelayDisappear());
    }

    IEnumerator DelayDisappear()
    {
        interactButton.GetComponent<Animator>().SetBool("get out", true);
        yield return new WaitForSeconds(1f);
        interactButton.SetActive(false);
        isInside = false;
    }

    private void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E) && check &&
            GameObject.Find("BAT 1") == null && GameObject.Find("BAT 2") == null && GameObject.Find("BAT 3") == null)
        {
            StartCoroutine(DelayEndScene());
        }
    }

    IEnumerator DelayEndScene()
    {
        transi.SetActive(true);
        check = false;
        interactButton.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
