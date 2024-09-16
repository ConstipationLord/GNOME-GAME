using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Material hitMaterial, normalMaterial;
    bool checkHit = false;
    bool isDead = false;
    public int batLives;
    void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Stick") && !checkHit && Input.GetMouseButton(0))
        {
            transform.GetComponent<SkinnedMeshRenderer>().material = hitMaterial;
            batLives--;
            checkHit = true;
            StartCoroutine(HitCoolDown());
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(0.3f);
        checkHit = false;
        transform.GetComponent<SkinnedMeshRenderer>().material = normalMaterial;
    }

    IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(0.3f);
        isDead = true;
        Destroy(gameObject);
    }
    private void Update()
    {
        if (batLives == 0 && !isDead)
        {
            StartCoroutine(DelayDie());
        }
    }
}
