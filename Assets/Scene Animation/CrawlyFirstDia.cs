using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CrawlyFirstDia : MonoBehaviour
{
    [SerializeField] private GameObject textbox;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private AudioSource dialogueSFX;
    [SerializeField] private Animator canvasAnim;

    public bool end = false;

    public int index;
    public bool check = false;
    public bool checkSound = false;
    Color newcol;

    // Start is called before the first frame update
    void Start()
    {
        newcol = bg.color;
        textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
        textComponent.text = string.Empty;
        StartCoroutine(BeginWait());
    }

    IEnumerator BeginWait()
    {
        yield return new WaitForSeconds(8f);
        canvasAnim.SetBool("end", true);
        check = true;
        checkSound = false;
        textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Crawly";
        bg.color = new Color(newcol.r, newcol.g, newcol.b, 1f);
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

    //IEnumerator DialogueSFX()
    //{
    //    foreach (char c in lines[index].ToCharArray())
    //    {
    //        if (textComponent.text != lines[index])
    //        {
    //            dialogueSFX.Play();
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //    }
    //}

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
            gameObject.GetComponent<CrawlyFirstDia>().enabled = false;
        }
    }
}
