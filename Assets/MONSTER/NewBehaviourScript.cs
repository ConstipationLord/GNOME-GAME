using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Material hitMaterial;
    bool checkHit = false;
    public int batLives;
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !checkHit && Input.GetMouseButton(0))
        {
            transform.parent.GetComponent<SkinnedMeshRenderer>().materials[0] = hitMaterial;
            batLives--;
            checkHit = true;
            StartCoroutine(HitCoolDown());
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(0.7f);
        checkHit = false;
    }
    private void Update()
    {
        if (batLives == 0)
        {
            Destroy(gameObject);
        }
    }
}
