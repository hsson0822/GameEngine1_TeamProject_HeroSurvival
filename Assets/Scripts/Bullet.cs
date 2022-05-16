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
        // �������ڸ��� ������ �߻�
        rigid = GetComponent<Rigidbody>();
        shotSpeed = 3000.0f;
        rigid.AddForce(transform.forward * shotSpeed);

        time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ���� �ð� �� �ڵ� �ı�
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

}
