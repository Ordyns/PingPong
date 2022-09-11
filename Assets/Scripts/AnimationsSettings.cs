[System.Serializable]
public class AnimationSettings<T>
{
    public T StartValue;
    public T TargetValue;
    [UnityEngine.Space]
    public float Duration;
    public DG.Tweening.Ease Ease;
}