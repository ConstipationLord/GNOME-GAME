using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Material hitMaterial, normalMaterial;
    bool checkHit = false;
    bool isDead = false;
    public int batLives;

    public AudioSource deadSFX;

    public GameObject parent;

    public GameObject[] trinkets;
    public Transform spawnPoint;

    void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Stick") && !checkHit)
        {
            if (Input.GetMouseButton(0))
            {
                transform.GetComponent<SkinnedMeshRenderer>().material = hitMaterial;
                batLives--;
                checkHit = true;
                StartCoroutine(HitCoolDown());
            }
            else if (Input.GetMouseButton(1))
            {
                transform.GetComponent<SkinnedMeshRenderer>().material = hitMaterial;
                batLives--;
                checkHit = true;
                StartCoroutine(UltiCoolDown());
            }
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(0.3f);
        checkHit = false;
        transform.GetComponent<SkinnedMeshRenderer>().material = normalMaterial;
    }
    IEnumerator UltiCoolDown()
    {
        yield return new WaitForSeconds(0.3f);
        transform.GetComponent<SkinnedMeshRenderer>().material = normalMaterial;

        yield return new WaitForSeconds(0.5f);
        batLives--;
        transform.GetComponent<SkinnedMeshRenderer>().material = hitMaterial;

        yield return new WaitForSeconds(0.3f);
        checkHit = false;
        transform.GetComponent<SkinnedMeshRenderer>().material = normalMaterial;
    }

    IEnumerator DelayDie()
    {
        deadSFX.Play();
        yield return new WaitForSeconds(0.3f);
        isDead = true;
        Instantiate(trinkets[Random.Range(0,7)], spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(0.15f);
        Destroy(parent);
    }
    private void Update()
    {
        if (batLives == 0 && !isDead)
        {
            StartCoroutine(DelayDie());
        }
    }
}
