using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int hp;
    private int exp;
    private int level;

    private float moveSpeed = 20.0f;
    float hAxis;
    float vAxis;
    Vector3 moveVec;

    private Slider expBar;

    private void Awake()
    {
        hp = 100;
        level = 1;
        exp = 0;

        expBar = GameObject.Find("Canvas").gameObject.transform.Find("ExpBar").GetComponent<Slider>();
        StartCoroutine(GetExp(0));

    }

    private void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //transform.Translate((Vector3.left + Vector3.forward) / 2 * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //transform.Translate((Vector3.right + Vector3.back) / 2 * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //transform.Translate((cam.transform.forward.normalized + cam.transform.right.normalized) * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //transform.Translate((Vector3.left + Vector3.back) / 2 * moveSpeed * Time.deltaTime);
        //}

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Vector3 v = new Vector3(vAxis, 0, vAxis).normalized;
        Vector3 h = new Vector3(hAxis, 0, -hAxis).normalized;
        moveVec = (h + v);

        transform.position += moveVec * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(GetExp(10));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(GetDamage(10));

            if (hp < 0)
                gameObject.SetActive(false);

            Debug.Log("HP : " + hp);
        }

        if (collision.gameObject.CompareTag("Exp"))
        {
            StartCoroutine(GetExp(10));
        }
    }

    IEnumerator GetDamage(int damage)
    {
        hp -= damage;

        yield return null;
    }

    IEnumerator GetExp(int e)
    {
        exp += e;
        expBar.value = (float)exp/1000;
        yield return null;
    }
}
