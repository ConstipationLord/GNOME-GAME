using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTriggeredCollision : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject textbox;
    [SerializeField] private AudioSource buttonSFX;

    bool isInside = false;
    bool check = false;

    private void OnTriggerEnter(Collider other)
    {
        isInside = true;
        check = true;
        interactButton.SetActive(true);
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
        if (isInside && Input.GetKeyDown(KeyCode.E) && check)
        {
            buttonSFX.Play();
            check = false;
            textbox.SetActive(true);
            textbox.GetComponent<BridgeDialogue>().enabled = true;
            interactButton.SetActive(false);
        }
    }
}
