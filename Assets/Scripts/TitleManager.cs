using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    static public TitleManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    // 게임씬으로 이동
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // 게임 종료
    public void ExitGame()
    {
        Debug.Log("AAA");
        Application.Quit();
       // UnityEditor.EditorApplication.isPlaying = false;
    }
}
