using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private Collectible collectiblePrefab;
    [SerializeField]
    private Transform spawnCentre;
    [SerializeField]
    private float spawnInterval = 1;
    [SerializeField]
    GameController gameControllerRef;
    [SerializeField]
    private float lifeCycle = 20;
    [SerializeField]
    private Transform ColllectiblesParentTf;
    private ObjectPool<Collectible> _pool;
    private int defaultPoolCapacity = 20;
    private Vector3 spawnOffset = new(0, 0, 20);
    public float LifeCycle
    {
        get { return lifeCycle; }
    }

    private void Awake()
    {
        InitializeCollectiblesObjectPool();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnObstaclesPeriodically());
    }


    private IEnumerator SpawnObstaclesPeriodically()
    {
        while (gameControllerRef.IsGameRunning)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        var collectible = _pool.Get();
        spawnOffset.y = Random.Range(0, 5);
        collectible.transform.position = spawnCentre.position + spawnOffset;
        collectible.transform.parent = ColllectiblesParentTf;
        collectible.gameObject.SetActive(true);
        // collectible.Init();
    }

    private void InitializeCollectiblesObjectPool()
    {
        _pool = new ObjectPool<Collectible>(CreateCollectible, null, OnReturnToPool, defaultCapacity: defaultPoolCapacity);//, defaultCapacity = defaultPoolCapacity)
    }

    private Collectible CreateCollectible()
    {
        var Collectible = Instantiate(collectiblePrefab);
        return Collectible;
    }

    private void OnReturnToPool(Collectible collectible)
    {
        collectible.gameObject.SetActive(false);
    }

    public void Release(Collectible collected)
    {
        _pool.Release(collected);
    }
}
