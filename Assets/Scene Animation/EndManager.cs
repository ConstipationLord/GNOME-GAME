using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject transi;

    public float speed;
    Vector3 add;
    bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        add = new Vector3(0, 0, 1) * Time.deltaTime * speed;
        StartCoroutine(DelayEnd());
    }

    // Update is called once per frame
    void Update()
    {
        bat.transform.position += add;
        if (check && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(4f);
        transi.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        check = true;
    }
}
