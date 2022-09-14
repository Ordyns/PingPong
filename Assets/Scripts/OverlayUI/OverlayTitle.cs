using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class OverlayTitle : MonoBehaviour
{   
    [Header("General")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private float defaultTitleDuration;

    [Header("Animation")]
    [SerializeField] private float animationDuration;
    [SerializeField] private Ease animationEase;
    [SerializeField] private float overshoot = 1;

    private Tweener _textTweener;
    private Timer _playBackwardsTimer;

    private void Awake() {
        titleText.transform.localScale = Vector3.zero;
        _textTweener = titleText.transform.DOScale(Vector3.one, animationDuration).SetEase(animationEase, overshoot).SetAutoKill(false).Pause();
    }

    public void ShowNewTitle(string title, float duration = 0){
        if(string.IsNullOrEmpty(title))
            throw new ArgumentNullException();

        if(duration == 0)
            duration = defaultTitleDuration;

        titleText.text = title;

        Animate(duration);
    }

    private void Animate(float titleDuration){
        if(_playBackwardsTimer?.IsRunning == true)
            _playBackwardsTimer.Dispose();

        _textTweener.Restart();
        _playBackwardsTimer = Timer.StartNew(this, titleDuration + animationDuration, _textTweener.PlayBackwards);
    }
}
