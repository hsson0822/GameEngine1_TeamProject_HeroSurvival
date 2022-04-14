using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent nav;
    GameObject target;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
    }
    void FixedUpdate()
    {
        nav.SetDestination(target.transform.position);
    }
}
