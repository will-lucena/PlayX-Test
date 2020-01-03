using System.Collections;
using UnityEngine;

public class UpdateCameraFocus : MonoBehaviour
{
    
    #region Serialized variables

    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;

    #endregion

    #region Private variables

    private Vector3 _positionOffset;

    #endregion

    #region Lifecycle methods

    private void OnEnable()
    {
        RoundController.onTreeSpawn += changeFocus;
        RoundController.initRound += restoreCameraInitialPosition;
    }

    private void OnDisable()
    {
        RoundController.onTreeSpawn -= changeFocus;
        RoundController.initRound -= restoreCameraInitialPosition;
    }
    
    void Start()
    {
        //With this offset the distance between the camera and the current tree will be the same as the initial even after the translation
        _positionOffset = transform.localPosition - Vector3.zero;
    }

    #endregion

    #region Delegates response methods

    public void changeFocus(Transform target)
    {
        StartCoroutine(translateCamera(target));
    }

    public void restoreCameraInitialPosition()
    {
        transform.localPosition = _positionOffset;
    }

    #endregion

    #region Coroutines

    //The animation will play based on the serialized curve and duration
    private IEnumerator translateCamera (Transform target)
    {
        float i = 0;
        float rate = 1 / duration;
        var localPosition = target.localPosition;
        Vector3 targetPosition = new Vector3(localPosition.x, 0, localPosition.z);
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp (transform.localPosition, targetPosition + _positionOffset, curve.Evaluate (i));
            yield return null;
        }
    }

    #endregion
}
