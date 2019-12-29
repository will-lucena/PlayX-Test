using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCameraFocus : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Transform firstTarget;
    
    private Vector3 positionOffset;

    private void OnEnable()
    {
        RoundController.onTreeDestroy += changeFocus;
    }

    private void OnDisable()
    {
        RoundController.onTreeDestroy -= changeFocus;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionOffset = transform.position - firstTarget.position;
    }

    private IEnumerator TranslateCamera (Transform target)
    {
        float i = 0;
        float rate = 1 / duration;
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp (transform.localPosition, target.position + positionOffset, curve.Evaluate (i));
            yield return null;
        }
    }

    public void changeFocus(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(TranslateCamera(target));
    }
}
