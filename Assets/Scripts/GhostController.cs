using System.Collections;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField]
    private float syncDelay = 0f;
    [SerializeField]
    [Range(1, 12)]
    private int syncInterpolationFrameRate;
    private Transform tf;
    private SyncData player1_SyncData;
    private StateSync stateSyncInstance;
    private Rigidbody rb;
    private Vector3 offsetPos;

    private void Awake()
    {
        player1_SyncData = new();
        tf = transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        offsetPos = new(0, 0, -syncDelay);
        stateSyncInstance = StateSync.Instance;
        StartCoroutine(UpdatePosition());
    }

    private IEnumerator UpdatePosition()
    {
        // yield return null;
        while (true)
        {
            FetchSyncData();
            // rb.MovePosition(targetPos);

            StartCoroutine(SmoothPosition(player1_SyncData.Position));

            int framesPassed = 0;
            while (framesPassed < syncInterpolationFrameRate)
            {
                framesPassed++;
                yield return null;
            }
        }
    }

    private IEnumerator SmoothPosition(Vector3 targetPos)
    {
        yield return new WaitForSeconds(syncDelay);
        float elapsedframes = 0;
        while (elapsedframes < syncInterpolationFrameRate)
        {
            float t = elapsedframes / syncInterpolationFrameRate;
            Vector3 lerpedPos = Vector3.Lerp(tf.position, targetPos, t);
            tf.position = lerpedPos;
            // rb.MovePosition(lerpedPos);
            elapsedframes += Time.deltaTime;
            yield return null;
        }
    }

    private void FetchSyncData()
    {
        if (stateSyncInstance == null)
        {
            player1_SyncData = new();
            return;
        }
        player1_SyncData = stateSyncInstance.Player1SyncData;
        Debug.Log(stateSyncInstance.Player1SyncData);
    }
}
