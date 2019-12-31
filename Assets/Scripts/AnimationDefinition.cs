using UnityEngine;

[CreateAssetMenu(fileName = "mew animation", menuName = "Animations")]
public class AnimationDefinition : ScriptableObject
{
    [SerializeField] private float _durattion;
    [SerializeField] private AnimationCurve _curve;
    public float durattion
    {
        get { return _durattion; }
    }

    public AnimationCurve curve
    {
        get { return _curve; }
    }
}
