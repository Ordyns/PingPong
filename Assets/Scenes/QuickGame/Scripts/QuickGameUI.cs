using UnityEngine;

public class QuickGameUI : MonoBehaviour
{
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private OverlayTitle overlayTitle;

    public void Init(ScoreViewModel scoreViewModel){
        scoreView.Init(scoreViewModel);

        scoreViewModel.RoundEnded += OnRoundEnded;
    }

    private void OnRoundEnded(RoundEndReason roundEndReason){
        switch(roundEndReason){
            case RoundEndReason.BallHitNet: overlayTitle.ShowNewTitle("Net!"); break;
            case RoundEndReason.UnsuccessfulServe: overlayTitle.ShowNewTitle("Bad serve"); break;
        }
    }
}