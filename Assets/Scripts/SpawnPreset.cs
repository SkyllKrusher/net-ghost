using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class SpawnPreset : MonoBehaviour
{
    int presetIndex;
    public int PresetIndex { get { return presetIndex; } }
    GroundController groundControllerRef;
    private float timePassed;
    public void Init(GroundController groundController, int presetIndex)
    {
        timePassed = 0;
        groundControllerRef = groundController;
        this.presetIndex = presetIndex;
    }
    private void Update()
    {
        ReleaseOnLifeCycleEnd();
    }
    private void ReleaseOnLifeCycleEnd()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= groundControllerRef.LifeCycle)
        {
            EndLifeCycle();
        }
    }
    private void EndLifeCycle()
    {
        groundControllerRef.Release(this);
    }
}
