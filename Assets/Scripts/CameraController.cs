using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform trans;
    GameObject player;

    public Vector3 CamRotation;
    public Vector3 CamPosition;

    private void Start()
    {
        // 카메라의 위치 초기화
        trans = gameObject.GetComponent<Transform>();
        player = GameObject.Find("Player");
        CamRotation = new Vector3(45, 45, 0);
        CamPosition = new Vector3(-10, 15, -10);
    }

    private void LateUpdate()
    {
        // 플레이어를 따라 움직임
        transform.position = player.transform.position + CamPosition;
        transform.localRotation = Quaternion.Euler(CamRotation);     //원하는 카메라 회전값 적용
    }
}
