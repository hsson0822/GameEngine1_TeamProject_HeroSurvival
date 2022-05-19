using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Weapon
{
    Gun,
    Satellite
}

public class Player : MonoBehaviour
{
    private int hp;
    private int exp;
    private int level;

    private Dictionary<Weapon, int> weaponLevel;
    public GameObject satel;

    bool pause = false;

    public float moveSpeed = 20.0f;
    bool dashDown;          // Get key left shift 

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    Animator anim;

    private Slider expBar;

    private GameObject shotPos;
    public GameObject Bullet;
    private float shotTime;

    private void Awake()
    {
        hp = 100;
        level = 1;
        exp = 0;
        
        weaponLevel = new Dictionary<Weapon, int>();

        expBar = GameObject.Find("Canvas").gameObject.transform.Find("ExpBar").GetComponent<Slider>();

        anim = GetComponent<Animator>();

        shotPos = transform.Find("ShotPos").gameObject;
        shotTime = 0f;

        StartCoroutine(ShotBullet());
    }

    private void FixedUpdate()
    {

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Vector3 v = new Vector3(vAxis, 0, vAxis).normalized;
        Vector3 h = new Vector3(hAxis, 0, -hAxis).normalized;
        moveVec = (h + v);

        if (dashDown)// moveSpeed * 2.0 
            transform.position += moveVec * moveSpeed * 2.0f * Time.deltaTime;
        else
            transform.position += moveVec * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.V))
        {
            satel = GameObject.Instantiate(satel);
           // satel.transform.position = transform.position;
            //satel.transform.position.x += new Vector3(3f, 0f, 0f);
        }

    }

    private void Update()
    {
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isDash", dashDown);

        dashDown = Input.GetButton("Dash");

        transform.LookAt(transform.position + moveVec); // round angel 

    }


    private void OnCollisionEnter(Collision collision)
    {
        // 적과 충돌 처리
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(10);

            if (hp < 0)
                gameObject.SetActive(false);

            Debug.Log("HP : " + hp);
        }

        // 경험치볼과의 충돌 처리
        if (collision.gameObject.CompareTag("Exp"))
        {
            GetExp(10);
            if (exp > 100)
            {
                Time.timeScale = 0.0f;
            }
            collision.gameObject.SetActive(false);
        }
        
        // 아이템과의 충돌처리
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    // 경험치 추가 함수
    void GetExp(int e)
    {
        exp += e;
        expBar.value = (float)exp / 1000;
    }

    // 데미지 함수
    void GetDamage(int damage)
    {
        hp -= damage;
    }

    // 총알 발사 코루틴
    IEnumerator ShotBullet()
    {
        // 일정 시간마다 자동 발사
        while (true)
        {
            shotTime += Time.deltaTime;

            if (shotTime > 2.0f)
            {
                GameObject temp = Instantiate(Bullet, shotPos.transform.position, transform.rotation);
                shotTime = 0f;
            }
            yield return null;
        }

    }
}
