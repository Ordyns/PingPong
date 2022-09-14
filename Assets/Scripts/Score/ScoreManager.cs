using System.Collections.Generic;

public class ScoreManager
{
    private List<IScoreHandler> _scoreHandlers = new List<IScoreHandler>();

    public void Subscribe(IScoreHandler scoreHandler) => _scoreHandlers.Add(scoreHandler);
    public void Unsubscribe(IScoreHandler scoreHandler) => _scoreHandlers.Remove(scoreHandler);

    private ScoreViewModel _scoreViewModel;
    private Ball _ball;
    private ServeManager _serveManager;

    public ScoreManager(ScoreViewModel scoreViewModel, ServeManager serveManager){
        _scoreViewModel = scoreViewModel;
        _serveManager = serveManager;
        _ball = serveManager.Ball;

        _serveManager.UnsuccessfulServe += (serveOwner) => EndRound(RoundEndReason.UnsuccessfulServe);
        _ball.BallEnteredIntoDeathZone += (zoneType) => {
            EndRound(zoneType == BallDeathZone.ZoneType.Net ? RoundEndReason.BallHitNet : RoundEndReason.BallLeftTheGamingArea);
        };
    }

    private void EndRound(RoundEndReason reason){
        switch(reason){
            case RoundEndReason.BallHitNet: AddPoint(_ball.LastHitter, false); break;
            case RoundEndReason.UnsuccessfulServe: AddPoint(_serveManager.CurrentServeOwner, false); break;
            case RoundEndReason.BallLeftTheGamingArea: OnBallLeftTheGamingArea(); break;
        }

        _scoreViewModel.OnRoundEnded(reason);
    }

    public void OnBallLeftTheGamingArea(){
        var hittedZonesOwners = _ball.HittedTableZonesOwners;
        bool addPointToLastHiiter = false;

        if(_ball.BallState == Ball.State.Serving)
            addPointToLastHiiter = _serveManager.IsServeTechnicallyCorrect();
        else if(hittedZonesOwners.Count == 1 && hittedZonesOwners.Contains(_ball.LastHitter) == false)
            addPointToLastHiiter = true;
        
        AddPoint(_ball.LastHitter, addPointToLastHiiter);
    }

    private void AddPoint(Racket.Owner ballLastHitter, bool addPointToLastHiiter){
        if(ballLastHitter == Racket.Owner.Player && addPointToLastHiiter == false)
            _scoreViewModel.AddPointToEnemy();
        else if(ballLastHitter == Racket.Owner.Bot && addPointToLastHiiter)
            _scoreViewModel.AddPointToEnemy();
        else
            _scoreViewModel.AddPointToPlayer();

        ScoreUpdated();
    }

    private void ScoreUpdated(){
        for(int i = 0; i < _scoreHandlers.Count; i++)
            _scoreHandlers[i].ScoreUpdated();
    }
}

public enum RoundEndReason{
    BallHitNet,
    BallLeftTheGamingArea,
    UnsuccessfulServe,
    Other
}