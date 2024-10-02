using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFacePlayer : MonoBehaviour
{
    public Transform target;
    private void Update()
    {

        //Vector3 direction = transform.localPosition - target.localPosition;
        //Quaternion rotation = Quaternion.LookRotation(direction);
        Vector3 rotation = new(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(rotation);
    }
}
