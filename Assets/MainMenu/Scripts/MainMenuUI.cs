using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup playButton;
    [SerializeField] private CanvasGroup backButton;
    [Space]
    [SerializeField] private CanvasGroup difficultySelection;

    private void Awake() {
        difficultySelection.alpha = 0;
        backButton.alpha = 0;
    }

    public void OpenDifficultySelection(){
        AnimateFade(playButton, 0);
        AnimateFade(difficultySelection, 1);
        AnimateFade(backButton, 1);
    }

    public void CloseAll(){
        AnimateFade(playButton, 1);
        AnimateFade(difficultySelection, 0);
        AnimateFade(backButton, 0);
    }

    private void AnimateFade(CanvasGroup canvasGroup, float targetAlpha){
        canvasGroup.DOFade(targetAlpha, 0.2f).SetEase(Ease.InOutSine);
    }
}