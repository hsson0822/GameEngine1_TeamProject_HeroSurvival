using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public int damage { get; set; }

    GameObject player;

    private void OnEnable()
    {
        player = GameObject.Find("Player").gameObject;
        //damage = 10;
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            transform.position = player.transform.position;
            transform.Translate(new Vector3(2.0f, 1f, 0f));
            transform.Rotate(player.transform.up, 1f);
            yield return null;
        }
    }
}
