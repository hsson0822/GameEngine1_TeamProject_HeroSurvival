using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//투사체 회전 
public class Item : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
