using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public GameObject item;
    private GameObject itemParent;
    private float itemTime = 5.0f;
    private float count = 0.0f;

    private GameObject player;

    private void Awake()
    {
        itemParent = GameObject.Find("Items");
        player = GameObject.Find("Player");
    }

    void Update()
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
    }
}
