using System;
using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool isShaking = false;
    public AnimationCurve shakingCurve;
    public float duration = 0.3f;

    void Update()
    {
        if (isShaking)
        {
            StartCoroutine(Shaking());
            isShaking = false;
        }    
    }

    IEnumerator Shaking()
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = shakingCurve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + UnityEngine.Random.insideUnitCircle * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
