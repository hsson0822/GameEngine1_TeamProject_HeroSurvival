using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Weapon
{
    GunSpeed,
    GunPower,
    Satellite
}

public class Player : MonoBehaviour
{
    public int maxHp;
    private int hp;
    private int[] maxExp;
    private int exp;
    private int level;

    public float moveSpeed = 8.0f;
    bool dashDown;          // Get key left shift 

    public Dictionary<Weapon, int> weaponLevel;
    public GameObject satel;

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    Animator anim;

    private Slider expBar;

    private GameObject shotPos;
    public GameObject Bullet;
    private float shotTime;

    public GameObject window;

    private void Awake()
    {
        maxHp = hp = 100;
        level = 1;

        weaponLevel = new Dictionary<Weapon, int>();
        weaponLevel.Add(Weapon.GunSpeed, 0);
        weaponLevel.Add(Weapon.GunPower, 0);
        weaponLevel.Add(Weapon.Satellite, 0);

        maxExp = new int[10];
        InitExp();
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

        if (Input.GetKeyDown(KeyCode.V))
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
        // ???? ???? ????
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(10);

            if (hp < 0)
            {
                gameObject.SetActive(false);
                InGameManager.Instance.GameOverWindow.SetActive(true);
                InGameManager.Instance.TitleButton.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(0); });
            }

            //Debug.Log("HP : " + hp);
        }

        // ???????????? ???? ????
        if (collision.gameObject.CompareTag("Exp"))
        {
            collision.gameObject.SetActive(false);
            GetExp(100);
            //if (exp >= maxExp[level-1])
            if (exp >= 1000)    // 10?? -> 1?? 
            {
                //Debug.Log("exp" + exp);
               // Debug.Log("maxexp" + maxExp[level-1]);

                InGameManager.Instance.ItemSelectWindow.SetActive(true);


                Time.timeScale = 0.0f;
                InGameManager.Instance.isPause = true;

                ++level;
                exp = 0;
            }
        }

        // ?????????? ????????
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
            string itemName = collision.transform.name;
            TakeItem(itemName);
        }
    }

    void InitExp()
    {
        for (int i = 0; i < 10; ++i)
            maxExp[i] = (i+1) * 100;
    }

    // ?????? ???? ????
    void GetExp(int e)
    {
        exp += e;
        expBar.value = (float)exp / 1000;
    }

    // ?????? ????
    void GetDamage(int damage)
    {
        hp -= damage;
    }

    // ???? ???? ??????
    IEnumerator ShotBullet()
    {
        // ???? ???????? ???? ????
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

    void TakeItem(string item)
    {
        switch(item)
        {
            case "Item1":
            case "Item1(Clone)":
                hp += 50;
                if (hp > maxHp)
                    hp = maxHp;
                Debug.Log("???? ????");
                break;

            case "Item2(Clone)":
                break;

            //case "Item3(Clone)":
            //    break;

            //case "Item4(Clone)":
            //    break;
        }
    }

    public void SpawnSatellite()
    {
        Debug.Log("Satellite");
        satel = GameObject.Instantiate(satel);
    }
}
