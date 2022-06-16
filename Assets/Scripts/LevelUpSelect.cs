using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelect : MonoBehaviour
{
    enum UpgradeList
    {
        MaxHPUp,
        SpeedUp,
        GunSpeed,
        GunPower,
        SatellitePower
    }

    private GameObject[] button;
    UpgradeList[] lst;

    private void OnEnable()
    {
        button = new GameObject[3];
        lst = new UpgradeList[button.Length];

        for (int i = 0; i < button.Length; i++)
        {
            button[i] = gameObject.transform.GetChild(i + 1).gameObject;
            lst[i] = (UpgradeList)Random.Range(0, 5);
        }

        while(lst[0] == lst[1] || lst[1] == lst[2] || lst[2] == lst[0])
        {
            if(lst[0] == lst[1])
                lst[1] = (UpgradeList)Random.Range(0, 5);

            if (lst[1] == lst[2])
                lst[2] = (UpgradeList)Random.Range(0, 5);

            if(lst[2] == lst[0])
                lst[0] = (UpgradeList)Random.Range(0, 5);
        }

        for (int i = 0; i < button.Length; i++)
        {
            switch (lst[i])
            {
                case UpgradeList.MaxHPUp:
                    button[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "HP";
                    button[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+50";
                    button[i].GetComponent<Button>().onClick.AddListener(() => maxHpUp());
                    break;

                case UpgradeList.SpeedUp:
                    button[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Move Speed";
                    button[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+10";
                    button[i].GetComponent<Button>().onClick.AddListener(() => speedHp());
                    break;

                case UpgradeList.GunSpeed:
                    button[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Weapon Speed";
                    button[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+10";
                    button[i].GetComponent<Button>().onClick.AddListener(() => gunSpeedUp());
                    break;

                case UpgradeList.GunPower:
                    button[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Weapon Attack";
                    button[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+10";
                    button[i].GetComponent<Button>().onClick.AddListener(() => gunPowerUp());
                    break;

                case UpgradeList.SatellitePower:
                    button[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Satellite";
                    button[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "+1";
                    button[i].GetComponent<Button>().onClick.AddListener(() => satellitePowerUp());
                    break;
            }
        }
    }

    void maxHpUp()
    {
        InGameManager.Instance.player.GetComponent<Player>().maxHp += 10;
        Debug.Log(InGameManager.Instance.player.GetComponent<Player>().maxHp);
        selectEnd();
    }

    void speedHp()
    {
        InGameManager.Instance.player.GetComponent<Player>().moveSpeed += 10;
        selectEnd();
    }

    void gunSpeedUp()
    {
        InGameManager.Instance.player.GetComponent<Player>().weaponLevel[Weapon.GunSpeed]++;
        selectEnd();
    }

    void gunPowerUp()
    {
        InGameManager.Instance.player.GetComponent<Player>().weaponLevel[Weapon.GunPower]++;
        selectEnd();
    }

    void satellitePowerUp()
    {
        if (InGameManager.Instance.player.GetComponent<Player>().weaponLevel[Weapon.Satellite] == 0)
            InGameManager.Instance.player.GetComponent<Player>().SpawnSatellite();

        InGameManager.Instance.player.GetComponent<Player>().weaponLevel[Weapon.Satellite]++;
        selectEnd();
    }

    void selectEnd()
    {
        for (int i = 0; i < button.Length; ++i)
        {
            button[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }

        Time.timeScale = 1.0f;
        InGameManager.Instance.isPause = false;
        gameObject.SetActive(false);
    }
}
