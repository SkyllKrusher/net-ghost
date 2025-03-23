using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    private static GameFlow instance;
    public static GameFlow Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneNames.GameScene);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuScene);
    }

    internal void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

public struct SceneNames
{
    public const string MainMenuScene = "MainMenuScene";
    public const string GameScene = "GameScene";
}
