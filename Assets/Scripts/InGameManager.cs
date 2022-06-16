using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameManager : MonoBehaviour
{
    static public InGameManager Instance;

    private float itemTime = 5.0f;
    private float count = 0.0f;

    public GameObject player;

    private float playTime = 0.0f;
    private GameObject playTimeTxt;
    public bool isPause = false;

    public ObjectPoolingManager poolingManager;
    public GameObject ItemSelectWindow;
    public GameObject GameOverWindow;

    public GameObject TitleButton;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        player = GameObject.Find("Player");

        playTimeTxt = GameObject.Find("Canvas").transform.Find("PlayTime").gameObject;

        StartCoroutine(CheckTime());
        StartCoroutine(ItemControl());
        StartCoroutine(EnemyControl());

        ItemSelectWindow.SetActive(false);
        GameOverWindow.SetActive(false);
    }

    // 적의 생성을 관리하는 코루틴
    IEnumerator EnemyControl()
    {
        double time = 0.0f;

        int index = 0;

        while (true)
        {
            time += Time.deltaTime;

            if (time > 10.0f)
            {
                switch (index)
                {
                    case 0:
                        Wave("Enemy1");
                        break;

                    case 1:
                        Wave("Enemy2");
                        break;

                    case 2:
                        Wave("Enemy3");
                        break;

                    case 3:
                        Wave("Enemy4");
                        break;
                    case 4:
                        Wave("Enemy1");
                        break;

                    case 5:
                        Wave("Enemy2");
                        break;

                    case 6:
                        Wave("Enemy3");
                        break;

                    case 7:
                        Wave("Enemy4");
                        break;
                    case 8:
                        Wave("Enemy2");
                        break;
                    case 9:
                        Wave("Enemy2");
                        break;
                    case 10:
                        Wave("Enemy2");
                        break;

                    //case 4:
                    //Wave("Enemy1");
                    //break;

                    default:
                        break;
                }
                ++index;
                time = 0.0f;
            }

            yield return null;
        }
    }

    // 웨이브 때 몬스터의 위치를 랜덤으로 정하고 생성하는 함수
    void Wave(string objectName)
    {
        for (int i = 0; i < 30; ++i)
        {
            var rotation = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));

            float radius = 30f;
            Vector3 playerPosition = player.transform.position;

            float a = playerPosition.x;
            float b = playerPosition.z;

            float x = Random.Range(-radius + a, radius + a);
            float z_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
            z_b *= Random.Range(0, 2) == 0 ? -1 : 1;
            float z = z_b + b;

            Vector3 randomPosition = new Vector3(x, 1.0f, z);
            poolingManager.Get(objectName, randomPosition, Quaternion.Euler(rotation));

        }
    }

    IEnumerator ItemControl()
    {
        double time = 0.0f;

        int index = 0;

        while (true)
        {
            time += Time.deltaTime;

            if (time > itemTime)
            {
                index = Random.Range(0, 5);
                switch (index)
                {
                    case 0:
                        SpawnItem("Item1");
                        break;

                    case 1:
                        SpawnItem("Item1");
                        break;
                }
                time = 0.0f;
            }

            yield return null;
        }
    }

    void SpawnItem(string objectName)
    {
        
        float x = Random.Range(player.transform.position.x - 100.0f, player.transform.position.x + 100.0f);
        float z = Random.Range(player.transform.position.z - 100.0f, player.transform.position.z + 100.0f);
        Vector3 pos = new Vector3(x, 1, z);

        if(objectName.Equals("Item1(Clone)") || objectName.Equals("Item1"))
            poolingManager.Get(objectName, pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
        else
            poolingManager.Get(objectName, pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        

    }

    IEnumerator CheckTime()
    {
        while (true)
        {
            // 플레이 타임 코드
            playTime += Time.deltaTime;
            playTimeTxt.GetComponent<TextMeshProUGUI>().text = $"{playTime:N2}";
            yield return null;
        }
    }
}
