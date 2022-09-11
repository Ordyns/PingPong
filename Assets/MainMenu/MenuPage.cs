using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    [SerializeField] private PageAnimationSettings<Vector2> openingPositionAnimation;
    [SerializeField] private PageAnimationSettings<Vector2> closingPositionAnimation;
    [Space]
    [SerializeField] private bool closeInstantlyOnStart;

    private void Start() {
        if(closeInstantlyOnStart)
            CloseInstantly();
    }

    public void Open() => AnimatePosition(openingPositionAnimation);
    public void OpenInstantly() => AnimatePosition(openingPositionAnimation, true);

    public void Close() => AnimatePosition(closingPositionAnimation);
    public void CloseInstantly() => AnimatePosition(closingPositionAnimation, true);

    private void AnimatePosition(PageAnimationSettings<Vector2> animation, bool instantly = false){
        if(animation.IsInitialized == false)
            InitPositionAnimation(animation);

        if(instantly)
            transform.localPosition = animation.Value;

        transform.DOLocalMove(animation.Value, animation.Duration).SetEase(animation.Ease);
    }

    private void InitPositionAnimation(PageAnimationSettings<Vector2> animation){
        if(animation.TargetValue == AutoValue.Custom)
            return;

        Vector2 canvasSize = MainCanvasSizeProvider.Size;
        Vector2 value = new Vector2(){
            x = animation.TargetValue == AutoValue.CanvasWidth ? canvasSize.x : 0,
            y = animation.TargetValue == AutoValue.CanvasHeight ? canvasSize.y : 0
        };
        animation.Value = value * (animation.UseNegativeValue ? -1 : 1);
        animation.IsInitialized = true;
    }

    [System.Serializable]
    private class PageAnimationSettings<T>
    {
        public PageAnimationSettings(PageAnimationSettings<T> animationSettings){
            Value = animationSettings.Value;
            Duration = animationSettings.Duration;
            Ease = animationSettings.Ease;
        }

        public AutoValue TargetValue;
        [HideIf(nameof(TargetValue), AutoValue.Custom)] [AllowNesting] public bool UseNegativeValue;
        [ShowIf(nameof(TargetValue), AutoValue.Custom)] [AllowNesting] public T Value;
        [Space]
        public float Duration;
        public Ease Ease;

        [HideInInspector] public bool IsInitialized;
    }

    private enum AutoValue
    {
        Custom,
        CanvasWidth,
        CanvasHeight,
    }
}
