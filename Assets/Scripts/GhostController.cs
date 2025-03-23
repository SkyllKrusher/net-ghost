using System;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField]
    private float syncDelay = 0f;
    private Transform tf;
    private SyncData player1_SyncData;
    private StateSync stateSyncInstance;

    private void Awake()
    {
        player1_SyncData = new();
        tf = transform;
        stateSyncInstance = StateSync.Instance;
    }

    private void Update()
    {
        FetchSyncData();
        Vector3 offsetPos = new(0, 0, -syncDelay);
        tf.position = player1_SyncData.Position + offsetPos;
    }

    private void FetchSyncData()
    {
        player1_SyncData = stateSyncInstance.Player1SyncData;
    }
}
