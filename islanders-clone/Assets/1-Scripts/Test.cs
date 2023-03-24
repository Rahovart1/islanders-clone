using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private AnimationCurve _curve;
    void Start()
    {
        StartCoroutine(TestCoroutine());
    }
    private IEnumerator TestCoroutine()
    {
        float timeElapsed = 0f;
        while (timeElapsed < _duration)
        {
            timeElapsed += Time.deltaTime;
            transform.localScale = Vector3.one * _curve.Evaluate(timeElapsed / _duration);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
