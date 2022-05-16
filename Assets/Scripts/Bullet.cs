using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float shotSpeed;
    private Rigidbody rigid;
    private float time;

    void Start()
    {
        // 생성되자마자 앞으로 발사
        rigid = GetComponent<Rigidbody>();
        shotSpeed = 3000.0f;
        rigid.AddForce(transform.forward * shotSpeed);

        time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 일정 시간 후 자동 파괴
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

}
