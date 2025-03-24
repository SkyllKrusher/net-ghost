using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
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
    }

}
public class Tags
{
    public const string PLAYER_TAG = "Player";
}
