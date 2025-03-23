using UnityEngine;

public class StateSync : MonoBehaviour
{
    private static StateSync instance;
    public static StateSync Instance => instance;
    private SyncData player1SyncData;
    public SyncData Player1SyncData => player1SyncData;

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
        player1SyncData = new();
    }
    public void UpdateState(Vector3 position)
    {
        player1SyncData.Position = position;
    }
}

public class SyncData
{
    private Vector3 position;

    public SyncData()
    {
        position = new(0, 0, 0);
    }

    public SyncData(Vector3 syncPosition)
    {
        position = syncPosition;
    }

    public Vector3 Position { get => position; set => position = value; }
}
