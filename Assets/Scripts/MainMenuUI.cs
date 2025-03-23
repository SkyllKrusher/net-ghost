using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button exitButton;

    void Awake()
    {
        startButton.onClick.AddListener(() => OnClickStart());
        exitButton.onClick.AddListener(() => OnClickExit());
    }

    private void OnClickStart()
    {
        GameFlow.Instance.StartGame();
    }

    private void OnClickExit()
    {
        GameFlow.Instance.ExitGame();
    }
}
