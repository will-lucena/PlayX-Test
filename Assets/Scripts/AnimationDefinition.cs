using UnityEngine;

[CreateAssetMenu(fileName = "mew animation", menuName = "Animations")]
public class AnimationDefinition : ScriptableObject
{
    [SerializeField] private float _durattion;
    [SerializeField] private AnimationCurve _curve;
    
    public float durattion => _durattion;
    public AnimationCurve curve => _curve;
}
