using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int hp;

    NavMeshAgent nav;
    GameObject target;

    public ObjectPoolingManager poolingManager;

    private void Awake()
    {
        hp = 100;

        // Ÿ���� �÷��̾�� ����
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");

        poolingManager = GameObject.Find("ObjectPoolingManager").GetComponent<ObjectPoolingManager>();
    }
    void FixedUpdate()
    {
        // �����Ӹ��� �÷��̾� ��ġ ����
        nav.SetDestination(target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �Ѿ˰��� �浹ó��
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetDamage(10);
            Debug.Log("����");
            if(hp <= 0)
            {
                poolingManager.Get("ExpBall", transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                Hide();
            }
            Destroy(collision.gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Satellite"))
        {
            GetDamage(10);
            Debug.Log("����2");
            if (hp <= 0)
            {
                Hide();
                poolingManager.Get("ExpBall", transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
        
    }

    // ������Ʈ�� ��Ȱ��ȭ
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // ������ �Լ�
    public void GetDamage(int damage)
    {
        hp -= damage;
    }
}
