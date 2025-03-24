using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class Preset : MonoBehaviour
{
    int presetIndex;
    public int PresetIndex { get { return presetIndex; } }
    PresetSpawner presetSpawnerRef;
    private float timePassed;
    public void Init(PresetSpawner groundController, int presetIndex)
    {
        timePassed = 0;
        presetSpawnerRef = groundController;
        this.presetIndex = presetIndex;
    }
    private void Update()
    {
        ReleaseOnLifeCycleEnd();
    }
    private void ReleaseOnLifeCycleEnd()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= presetSpawnerRef.LifeCycle)
        {
            EndLifeCycle();
        }
    }
    private void EndLifeCycle()
    {
        presetSpawnerRef.Release(this);
    }
}
