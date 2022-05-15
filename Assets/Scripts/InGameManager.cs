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

        var position = new Vector3(Random.Range(50f, 100f), 1f, Random.Range(50f, 100f));
        var rotation = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
        poolingManager.Get("Enemy1", position, Quaternion.Euler(rotation));
    }

    void FixedUpdate()
    {
        count += Time.deltaTime;

        if(count > itemTime)
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
                switch(index)
                {
                    case 0:
                        break;

                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;
                }
                ++index;
                time = 0.0f;
            }

            yield return null;
        }
    }
}
