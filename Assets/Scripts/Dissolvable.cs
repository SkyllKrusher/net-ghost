using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Dissolvabe : MonoBehaviour
{
    [SerializeField]
    private float defaultDissolveDuration;
    private float dissolveStrength;
    private Material dissolveMaterial;

    public float DefaultDissolveDuration { get { return defaultDissolveDuration; } }

    private void Awake()
    {
        dissolveMaterial = GetComponent<Renderer>().material;
    }

    private IEnumerator DissolveSmoothly()
    {
        float elapsedTime = 0;

        while (elapsedTime <= defaultDissolveDuration)
        {
            float t = elapsedTime / defaultDissolveDuration;

            dissolveStrength = Mathf.Lerp(0, 1, t);
            SetDissolve();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void SetDissolve()
    {
        dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);
    }

    public void ResetDissolve()
    {
        dissolveStrength = 0;
        dissolveMaterial.SetFloat("_DissolveStrenght", 0);
    }

    public void StartDissolve()
    {
        StartCoroutine(DissolveSmoothly());
    }

    public void StartDissolve(float dissolveDuration)
    {
        this.defaultDissolveDuration = dissolveDuration;
        StartDissolve();
    }
}
