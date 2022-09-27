using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DropdownElement : MonoBehaviour, IPointerDownHandler
{
    public event System.Action<string> OnClick;
    public string Value { get; private set; }

    public Vector2 Size {
        get{
            if(_rectTransform == null)
                Awake();

            return _rectTransform.rect.size;
        }
    }

    [SerializeField] private TextMeshProUGUI contentText;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private const float AnimationDuration = 0.2f;
    private const Ease PositionAnimationEase = Ease.OutCubic;
    private const Ease FadeAnimationEase = Ease.InOutSine;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(string value, Vector2 targetPosition){
        if(string.IsNullOrEmpty(value))
            throw new System.ArgumentNullException();

        Value = value;
        contentText.text = value;

        _startPosition = transform.localPosition;
        _targetPosition = targetPosition;
    }

    public void Show(){
        _canvasGroup.interactable = true;
        _canvasGroup.DOFade(1, AnimationDuration).SetEase(FadeAnimationEase);
        _rectTransform.DOLocalMove(_targetPosition, AnimationDuration).SetEase(PositionAnimationEase);
    }

    public void Hide(){
        _canvasGroup.interactable = false;
        _canvasGroup.DOFade(0, AnimationDuration).SetEase(FadeAnimationEase);
        _rectTransform.DOLocalMove(_startPosition, AnimationDuration).SetEase(PositionAnimationEase);
    }

    public void OnPointerDown(PointerEventData eventData){
        if(_canvasGroup.interactable == false)
            return;

        OnClick?.Invoke(Value);
    }
}
