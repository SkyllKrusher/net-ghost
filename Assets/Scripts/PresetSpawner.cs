using UnityEngine;
using System.Collections;
using UnityEngine.Pool;
using System;
using Unity.VisualScripting;

public class PresetSpawner : MonoBehaviour
{
    [SerializeField]
    private Preset[] presetPrefabs;
    [SerializeField]
    private Transform spawnLocation;
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float lifeCycle;
    public float LifeCycle { get { return lifeCycle; } }
    private Vector3 spawnOffset = new(0, -2, 0);
    private ObjectPool<Preset>[] _presetPools;
    private bool spawn = true;
    private int initializePoolIndex;

    private void Awake()
    {
        CreateAllPresetPools();
    }
    private void CreateAllPresetPools()
    {
        _presetPools = new ObjectPool<Preset>[presetPrefabs.Length];
        initializePoolIndex = 0;
        for (int i = 0; i < presetPrefabs.Length; i++)
        {
            Debug.LogError("INIT POOL INDEX in loop = " + initializePoolIndex);

            _presetPools[i] = new ObjectPool<Preset>(CreatePresetPool, null, OnRelease, defaultCapacity: 20);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(SpawnGroundsPeriodically());
    }

    private void OnRelease(Preset preset)
    {
        preset.gameObject.SetActive(false);
    }

    private Preset CreatePresetPool()
    {
        Debug.LogError("INIT POOL INDEX = " + initializePoolIndex);
        var preset = Instantiate(presetPrefabs[initializePoolIndex]);
        preset.Init(this, initializePoolIndex);
        initializePoolIndex++;
        return preset;
    }

    private IEnumerator SpawnGroundsPeriodically()
    {
        int i = 0;
        while (spawn)
        {
            Debug.LogError("i = " + i);
            int roundedIndex = (i + 1) % (presetPrefabs.Length);
            Debug.Log("rounded i = " + roundedIndex);
            SpawnGround(roundedIndex);
            yield return new WaitForSeconds(spawnInterval);
            i++;
            spawnOffset.z += 170;
        }
    }

    private void SpawnGround(int index)
    {
        var ground = _presetPools[index].Get();
        ground.transform.position = spawnLocation.position + spawnOffset;
        ground.gameObject.SetActive(true);
    }

    public void Release(Preset spawnPreset)
    {
        _presetPools[spawnPreset.PresetIndex].Release(spawnPreset);
    }
}
