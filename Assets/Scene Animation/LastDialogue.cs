using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LastDialogue : MonoBehaviour
{
    [SerializeField] private GameObject textbox;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private AudioSource gnomeSFX, elfSFX, duckSFX, treeSFX;
    [SerializeField] private Animator canvasAnim;

    [SerializeField] private GameObject cam;
    [SerializeField] private Transform gnome, otherPoint;

    public bool end = false;

    public int index;
    public bool check, checkSound;

    AudioSource currentDiaSound;
    Color newcol;

    bool isFaceGnome;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        newcol = bg.color;
        textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
        textComponent.text = string.Empty;
        isFaceGnome = true;
        StartCoroutine(BeginWait());
    }
    IEnumerator BeginWait()
    {
        yield return new WaitForSeconds(8f);
        canvasAnim.SetBool("end", true);
        currentDiaSound = gnomeSFX;
        textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Crawly";
        textComponent.text = string.Empty;
        check = true;
        checkSound = false;
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
                currentDiaSound.Play();
                checkSound = true;
            }
            else if (textComponent.text == lines[index])
            {
                currentDiaSound.Stop();
                checkSound = false;
            }
        }

        if (isFaceGnome)
        {
            target = gnome;
        }
        else
        {
            target = otherPoint;
        }

        Vector3 rotation = new(target.position.x, cam.transform.position.y, target.position.z);
        cam.transform.LookAt(rotation);
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
            isFaceGnome = false;
            currentDiaSound = elfSFX;
            textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Some narcissist elf";
        }
        else if (index == 1)
        {
            currentDiaSound = duckSFX;
            textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Evolved Duck";
        }
        else if (index == 2)
        {
            currentDiaSound = treeSFX;
            textbox.transform.GetChild(0).GetComponent<TMP_Text>().text = "Random tree";
        }
        else if (index == 3)
        {
            isFaceGnome = true;
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
        }
    }
}
