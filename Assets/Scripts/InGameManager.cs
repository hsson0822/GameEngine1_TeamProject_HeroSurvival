using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameManager : MonoBehaviour
{
    static public InGameManager Instance;

    public GameObject item;
    private GameObject itemParent;
    private float itemTime = 5.0f;
    private float count = 0.0f;

    private GameObject player;

    private float playTime = 0.0f;
    private GameObject playTimeTxt;

    public ObjectPoolingManager poolingManager;


    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        itemParent = GameObject.Find("Items");
        player = GameObject.Find("Player");

        playTimeTxt = GameObject.Find("Canvas").transform.Find("PlayTime").gameObject;

        StartCoroutine(EnemyControl());

    }

    void FixedUpdate()
    {
        count += Time.deltaTime;

        if (count > itemTime)
        {
            float x = Random.Range(player.transform.position.x - 100.0f, player.transform.position.x + 100.0f);
            float z = Random.Range(player.transform.position.z - 100.0f, player.transform.position.z + 100.0f);
            Vector3 pos = new Vector3(x, 1, z);
            item.transform.position = pos;
            GameObject.Instantiate(item, itemParent.transform);
            count = 0;
        }


        playTime += Time.deltaTime;
        playTimeTxt.GetComponent<TextMeshProUGUI>().text = $"{playTime:N2}";
    }

    IEnumerator EnemyControl()
    {
        double time = 0.0f;

        int index = 0;

        //var position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        //var rotation = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
        //poolingManager.Get(objectName, position, Quaternion.Euler(rotation));

        while (true)
        {
            time += Time.deltaTime;

            if (time > 50.0f)
            {
                //switch (index)
                //{
                //    case 0:
                //        Wave();
                //        break;

                //    case 1:
                //        break;

                //    case 2:
                //        break;

                //    case 3:
                //        break;

                //    case 4:
                //        break;
                //}
                Wave();
                ++index;
                time = 0.0f;
            }

            yield return null;
        }
    }

    void Wave()
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
            poolingManager.Get("Enemy1", randomPosition, Quaternion.Euler(rotation));

        }
    }
}
