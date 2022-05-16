using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    int hp;

    NavMeshAgent nav;
    GameObject target;

    private void Awake()
    {
        hp = 100;

        // Ÿ���� �÷��̾�� ����
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
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
                Hide();
                Destroy(collision.gameObject);
            }
        }
    }

    // ������Ʈ�� ��Ȱ��ȭ
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // ������ �Լ�
    void GetDamage(int damage)
    {
        hp -= damage;
    }
}
