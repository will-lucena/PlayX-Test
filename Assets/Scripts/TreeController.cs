using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    //To avoid script imports I used some delegates, so it won't crash if the other scripts doesn't exist
    #region Delagates

    public static Func<Vector3, Vector3, float, AnimationCurve, IEnumerator> onAnimate;
    public Action onDestroy;
    public Action<Transform> onAnimationEnd;
    public static Action<Vector3> initParticles;

    #endregion
    
    #region Serialized variables

    [SerializeField] private GameObject trunkPrefab;
    [SerializeField] private float trunkOffset;
    [SerializeField] private AnimationDefinition popup;
    [SerializeField] private AnimationDefinition bounce;
    [SerializeField] private AnimationDefinition collapse;
    [SerializeField] private float baseOffset;

    #endregion

    #region Private variables

    private Queue<GameObject> _trunks;

    #endregion

    #region Lifecycle methods

    private void OnEnable()
    {
        RoundController.onDestroyTrunk += dequeueTrunk;
    }

    private void OnDisable()
    {
        RoundController.onDestroyTrunk -= dequeueTrunk;
    }

    #endregion
    
    #region Coroutines

    //The first part of the tree spawn animation, when it finishes call the second
    private IEnumerator growthAnimation(int height)
    {
        if (onAnimate != null)
        {
            var localPosition = transform.localPosition;
            Vector3 initialPosition = new Vector3(localPosition.x, -trunkOffset * height, localPosition.z);
            Vector3 finalPosition = new Vector3(localPosition.x, baseOffset, localPosition.z);
            yield return onAnimate.Invoke(initialPosition, finalPosition, popup.durattion,
                popup.curve);
        }
        yield return bounceAnimation();
    }

    //The second part of the tree spawn animation, when it finishes call notify the roundController to move the camera
    private IEnumerator bounceAnimation()
    {
        if (onAnimate != null)
        {
            var localPosition = transform.localPosition;
            Vector3 finalPosition = new Vector3(localPosition.x, localPosition.y + 2f, localPosition.z);
            yield return onAnimate.Invoke(localPosition, finalPosition, bounce.durattion,
                bounce.curve);
        }
        onAnimationEnd?.Invoke(transform);
    }

    //The animation played when one trunk is destroyed, the ui buttons are disabled while the animation runs
    private IEnumerator collapseAnimation()
    {
        RoundController.updateButtonState?.Invoke(false);
        if (onAnimate != null)
        {
            var localPosition = transform.localPosition;
            Vector3 finalPosition = new Vector3(localPosition.x, localPosition.y - trunkOffset, localPosition.z);
            yield return onAnimate.Invoke(localPosition, finalPosition, collapse.durattion,
                collapse.curve);
        }
        RoundController.updateButtonState?.Invoke(true);
    }

    #endregion

    #region Delegates response methods

    public void init(Vector3 position, int height)
    {
        transform.localPosition = new Vector3(position.x, baseOffset, position.z);
        _trunks = new Queue<GameObject>();
        for (int i = 0; i < height; i++)
        {
            GameObject trunk = Instantiate(trunkPrefab, transform);
            trunk.transform.localPosition = new Vector3(0, i * trunkOffset, 0);
            trunk.transform.localRotation = Quaternion.Euler(90, 0, 0);
            _trunks.Enqueue(trunk);
        }
        StartCoroutine(growthAnimation(height));
    }
    
    //Remove the bottom trunk, animating the process, if the last trunk was removed, notify the listener
    private void dequeueTrunk()
    {
        GameObject trunk = _trunks.Dequeue();
        initParticles?.Invoke(transform.localPosition);
        trunk.GetComponent<Trunk>().explode();
        StartCoroutine(collapseAnimation());
        if (_trunks.Count == 0)
        {
            onDestroy?.Invoke();
        }
    }

    #endregion
}
