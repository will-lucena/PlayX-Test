using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundAnimation : MonoBehaviour
{
    #region Serialized variables

    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Color fade;

    #endregion

    public void startAnimation()
    {
        StartCoroutine(roundTransition());
    }
    
    #region Coroutines

    //The animation will play based on the serialized curve and duration
    private IEnumerator roundTransition()
    {
        float i = 0;
        float rate = 1 / duration;
        while (i < 1) {
            i += rate * Time.deltaTime;
            fadeImage.color = Color.Lerp (Color.clear, fade, curve.Evaluate (i));
            yield return null;
        }
    }
    
    #endregion
}
