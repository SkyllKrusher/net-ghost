using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameUI gameUIReference;
    [Space]
    [SerializeField]
    private float timeToSpeed;
    [SerializeField]
    [Range(1f, 10f)]
    private float speedMuliplyFactor = 1.2f;

    private float distanceScore;
    private int collectedScore;
    private int totalScore;
    private int scorePerCollection = 25;
    private float scoreUpdateTime = 0.2f;

    private bool isGameRunning = true;
    private float gameSpeed = 1f;
    public float GameSpeed => gameSpeed;


    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        isGameRunning = true;
        collectedScore = 0;
        Coroutine increaseScoreCoroutine = StartCoroutine(IncreaseDistanceScore());
        Coroutine speedUpGameCoroutine = StartCoroutine(SpeedUpGamePeriodically());
    }

    private IEnumerator SpeedUpGamePeriodically()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(timeToSpeed);

            SpeedUpGame();
        }
    }

    private IEnumerator IncreaseDistanceScore()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(scoreUpdateTime);
            distanceScore += gameSpeed;
            UpdateScore();
        }
    }

    private void SpeedUpGame()
    {
        gameSpeed *= speedMuliplyFactor;

    }
    public void CollectPoint()
    {
        collectedScore += scorePerCollection;
        UpdateScore();
    }

    public void UpdateScore()
    {
        totalScore = (int)distanceScore + collectedScore;
        gameUIReference.UpdateScore(totalScore);
    }
}
