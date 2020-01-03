using System.Collections;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    #region Lifecycle methods

    private void OnEnable()
    {
        TreeController.onAnimate += translateObject;
    }

    private void OnDisable()
    {
        TreeController.onAnimate -= translateObject;
    }

    #endregion

    #region Delegates response methods

    //The animation will play based on the serialized curve and duration
    private IEnumerator translateObject (Vector3 initialPosition, Vector3 finalPosition, float duration, AnimationCurve curve)
    {
        float i = 0;
        float rate = 1 / duration;
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp (initialPosition, finalPosition, curve.Evaluate (i));
            yield return null;
        }
    }

    #endregion
}
