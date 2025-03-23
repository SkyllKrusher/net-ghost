using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    // [SerializeField]
    // private TMP_Text scoreAddText;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
