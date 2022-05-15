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

        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
    }
    void FixedUpdate()
    {
        nav.SetDestination(target.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetDamage(10);
            Debug.Log("¸ÂÀ½");
            if(hp <= 0)
            {
                Hide();
                Destroy(collision.gameObject);
            }
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    void GetDamage(int damage)
    {
        hp -= damage;
    }
}
