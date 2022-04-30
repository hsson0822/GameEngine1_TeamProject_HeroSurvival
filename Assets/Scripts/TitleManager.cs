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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("AAA");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
