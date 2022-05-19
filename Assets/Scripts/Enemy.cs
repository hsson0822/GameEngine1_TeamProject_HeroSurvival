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

        // 타겟을 플레이어로 설정
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");

        poolingManager = GameObject.Find("ObjectPoolingManager").GetComponent<ObjectPoolingManager>();
    }
    void FixedUpdate()
    {
        // 프레임마다 플레이어 위치 추적
        nav.SetDestination(target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 총알과의 충돌처리
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetDamage(10);
            Debug.Log("맞음");
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
            Debug.Log("맞음2");
            if (hp <= 0)
            {
                Hide();
                poolingManager.Get("ExpBall", transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
        
    }

    // 오브젝트가 비활성화
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // 데미지 함수
    public void GetDamage(int damage)
    {
        hp -= damage;
    }
}
