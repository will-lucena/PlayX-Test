using System.Collections;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        TreeController.onAnimate += TranslateObject;
    }

    private void OnDisable()
    {
        TreeController.onAnimate -= TranslateObject;
    }

    private IEnumerator TranslateObject (Vector3 initialPosition, Vector3 finalPosition, float duration, AnimationCurve curve)
    {
        float i = 0;
        float rate = 1 / duration;
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp (initialPosition, finalPosition, curve.Evaluate (i));
            yield return null;
        }
    }
}
