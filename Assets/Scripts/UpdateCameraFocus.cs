using System.Collections;
using UnityEngine;

public class UpdateCameraFocus : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    
    private Vector3 positionOffset;

    private void OnEnable()
    {
        RoundController.onTreeSpawn += changeFocus;
        RoundController.initRound += resetCamera;
    }

    private void OnDisable()
    {
        RoundController.onTreeSpawn -= changeFocus;
        RoundController.initRound -= resetCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionOffset = transform.localPosition - Vector3.zero;
    }

    private IEnumerator TranslateCamera (Transform target)
    {
        float i = 0;
        float rate = 1 / duration;
        Vector3 targetPosition = new Vector3(target.localPosition.x, 0, target.localPosition.z);
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp (transform.localPosition, targetPosition + positionOffset, curve.Evaluate (i));
            yield return null;
        }
    }

    public void changeFocus(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(TranslateCamera(target));
    }

    public void resetCamera()
    {
        transform.localPosition = positionOffset;
    }
}
