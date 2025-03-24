using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float timePassed = 0;
    CollectibleController collectibleController;
    public void Awake()
    {
        collectibleController = FindFirstObjectByType<CollectibleController>();
    }

    public void Init()
    {
        timePassed = 0;
        // gameObject.SetActive(true);
    }

    private void Update()
    {
        ReleaseOnLifeCyleEnd();
    }
    private void ReleaseOnLifeCyleEnd()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= collectibleController.LifeCycle)
        {
            ReturnToObjectPool();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("other tag" + other.tag);
        if (other.CompareTag(Tags.PLAYER_TAG))
        {
            Collect(other.GetComponent<PlayerController>());
        }
    }

    private void Collect(PlayerController player)
    {
        Debug.Log("Collect Cube");
        player.CollectPoint();
        ReturnToObjectPool();
    }
    private void ReturnToObjectPool()
    {
        collectibleController.Release(this);
    }
}

public class Tags
{
    public const string PLAYER_TAG = "Player";
}