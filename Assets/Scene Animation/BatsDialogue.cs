using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatsDialogue : MonoBehaviour
{
    [SerializeField] private GameObject textbox;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private AudioSource dialogueSFX;

    public bool end = false;

    public int index;
    public bool check, checkSound;
    bool check2 = false;

    // Start is called before the first frame update
    void Start()
    {
        check = false;
        check2 = false;
        checkSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("BAT 1") == null && GameObject.Find("BAT 2") == null && GameObject.Find("BAT 3") == null && !check2)
        {
            textbox.SetActive(true);
            check2 = true;
            textComponent.text = string.Empty;
            check = true;
            StartDialogue();
        }

        if (check)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
            if (textComponent.text != lines[index] && !checkSound)
            {
                dialogueSFX.Play();
                checkSound = true;
            }
            else if (textComponent.text == lines[index])
            {
                dialogueSFX.Stop();
                checkSound = false;
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //type each character one by one
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {

        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            check = false;
            textbox.SetActive(false);
        }
    }
}
