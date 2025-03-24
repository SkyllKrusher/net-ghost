using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private Dissolvabe dissolveObjectRef;
    private float waitTimeBeforeDestroy = 0;
    public void OnHit()
    {
        dissolveObjectRef.StartDissolve();
        waitTimeBeforeDestroy = dissolveObjectRef.DefaultDissolveDuration;
        StartCoroutine(DestroyObstacleAfterWait());
    }

    private IEnumerator DestroyObstacleAfterWait()
    {
        yield return new WaitForSeconds(waitTimeBeforeDestroy);
        gameObject.SetActive(false);
    }
}
