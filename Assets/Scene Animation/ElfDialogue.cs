using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElfDialogue : MonoBehaviour
{
    [SerializeField] private GameObject textbox;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private AudioSource gnomeSFX, elfSFX;

    public bool end = false;

    public int index;
    public bool check, checkSound;

    AudioSource currentDiaSound;

    // Start is called before the first frame update
    void Start()
    {
        currentDiaSound = gnomeSFX;
        textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Crawly";
        textComponent.text = string.Empty;
        check = true;
        checkSound = false;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
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
                currentDiaSound.Play();
                checkSound = true;
            }
            else if (textComponent.text == lines[index])
            {
                currentDiaSound.Stop();
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
        if (index == 0)
        {
            currentDiaSound = elfSFX;
            textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Some narcissist elf";
        }
        else if (index == 2)
        {
            currentDiaSound = gnomeSFX;
            textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Crawly";
        }

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
            gameObject.GetComponent<BridgeDialogue>().enabled = false;
        }
    }
}
