using System.Collections;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    #region Serialized variables

    [SerializeField] private Color flashColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private AnimationCurve colorCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private float duration;

    #endregion

    #region Private variables

    private Renderer _renderer;
    private Vector3 _initialScale;

    #endregion

    #region Lifecycle methods

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _initialScale = transform.localScale;
    }

    #endregion

    #region Coroutines

    private IEnumerator takeDamage ()
        {
            float i = 0;
            float rate = 1 / duration;
            while (i < 1) {
                i += rate * Time.deltaTime;
                transform.localScale = Vector3.Lerp (_initialScale, finalScale, scaleCurve.Evaluate (i));
                _renderer.material.color = Color.Lerp(defaultColor, flashColor, colorCurve.Evaluate(i));
                yield return null;
            }
            Destroy(gameObject);
        }

    #endregion
    
    public void explode()
    {
        StartCoroutine(takeDamage());
    }
}
