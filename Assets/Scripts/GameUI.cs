using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private GameObject gameOverUI;
    // [SerializeField]
    // private TMP_Text scoreAddText;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(() => OnClickRestart());
        mainMenuButton.onClick.AddListener(() => OnClickMainMenu());
    }

    private void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void OnClickRestart()
    {
        GameFlow.Instance.StartGame();

    }

    public void OnClickMainMenu()
    {
        GameFlow.Instance.GoToMainMenu();
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }
}
