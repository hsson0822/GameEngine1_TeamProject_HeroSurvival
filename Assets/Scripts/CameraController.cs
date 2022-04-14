using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform trans;
    GameObject player;

    public Vector3 CamRotation = new Vector3(45, 0, 0);
    public Vector3 CamPosition;

    private void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        player = GameObject.Find("Player");
        CamPosition = new Vector3(0, 15, -15);
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + CamPosition;
        transform.localRotation = Quaternion.Euler(CamRotation);     //원하는 카메라 회전값 적용
    }
}
