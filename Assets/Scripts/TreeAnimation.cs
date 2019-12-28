using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    public static Action onEndAnimation;
    
    [SerializeField] private AnimationCurve linearCurve;
    [SerializeField] private AnimationCurve popupCurve;
    [SerializeField] private AnimationCurve bounceCurve;
    
    [Header ("Target size")]
    [SerializeField] private Vector3 finalPosition;
    [Header ("Animation duration")]
    [SerializeField] private float duration;
    
    private Vector3 initialPosition;

    private void OnEnable()
    {
        TreeController.onStartAnimate += updateInitialPosition;
    }

    private void OnDisable()
    {
        TreeController.onStartAnimate -= updateInitialPosition;
    }

    private IEnumerator TranslateObject (AnimationCurve curve)
    {
        float i = 0;
        float rate = 1 / duration;
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp (initialPosition, finalPosition, curve.Evaluate (i));
            yield return null;
        }
        onEndAnimation?.Invoke();
    }

    public void startAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(TranslateObject(linearCurve));
    }

    public void bounceAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(TranslateObject(bounceCurve));
    }

    public void popupAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(TranslateObject(popupCurve));
    }
    
    public void updateInitialPosition(Vector3 pos)
    {
        initialPosition = pos;
        startAnimation();
    }
}
