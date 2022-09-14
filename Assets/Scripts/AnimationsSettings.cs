using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class AnimationSettings<T>
{
    public bool AnimateFromStartValue;
    [AllowNesting] [ShowIf(nameof(AnimateFromStartValue))] public T StartValue;
    public T TargetValue;
    [Space]
    public float Duration;
    public DG.Tweening.Ease Ease;
}