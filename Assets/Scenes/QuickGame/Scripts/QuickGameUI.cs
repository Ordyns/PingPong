using UnityEngine;

public class QuickGameUI : MonoBehaviour
{
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private OverlayTitle overlayTitle;
    [SerializeField] private OverlayTitleFactory overlayTitleFactory;

    public void Init(ScoreViewModel scoreViewModel){
        scoreView.Init(scoreViewModel);

        scoreViewModel.RoundEnded += OnRoundEnded;
    }

    private void OnRoundEnded(RoundEndReason roundEndReason){
        overlayTitle.ShowNewTitle(overlayTitleFactory.GetTitle(roundEndReason));
    }
}