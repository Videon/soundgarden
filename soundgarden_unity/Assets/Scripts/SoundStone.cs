using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundStone : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameter;

    public Color color1;

    public Color color0;

    public Renderer[] renderers;
    private static readonly int Color1 = Shader.PropertyToID("_EmissionColor");

    public Vector2 detectMinMax;

    private bool isDetecting = false;

    public float radius;

    public void StartBeat()
    {
        StartCoroutine(Beat());
    }

    private IEnumerator Beat()
    {
        float duration = 0.2f;


        float elapsedTime = 0f;


        while (elapsedTime < duration)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetColor(Color1, Color.Lerp(color0, color1, elapsedTime / duration));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    private IEnumerator Detect(Collider other)
    {
        isDetecting = true;
        Transform target = other.transform;
        while (isDetecting)
        {
            mixer.SetFloat(parameter,
                Mathf.Lerp(detectMinMax.x, detectMinMax.y,
                    1 - (Vector3.Distance(transform.position, target.position) / radius)));
            yield return null;
        }

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDetecting)
            StartCoroutine(Detect(other));
    }

    private void OnTriggerExit(Collider other)
    {
        isDetecting = false;
    }
}