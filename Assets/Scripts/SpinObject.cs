using UnityEngine;

public class SpinObject : MonoBehaviour
{
    private float spinX = 15;
    // float framesPassed = 0;
    private void Update()
    {
        // framesPassed++;

        transform.Rotate(spinX * Time.deltaTime, spinX * Time.deltaTime, 0);
    }
}
