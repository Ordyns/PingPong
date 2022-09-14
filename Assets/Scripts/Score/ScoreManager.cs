using System.Collections.Generic;

public class ScoreManager
{
    private LinkedList<IScoreHandler> _scoreHandlers = new LinkedList<IScoreHandler>();

    public void Subscribe(IScoreHandler scoreHandler) => _scoreHandlers.AddLast(scoreHandler);
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
        if(addPointToLastHiiter)
            _scoreViewModel.AddPointTo(ballLastHitter);
        else
            _scoreViewModel.AddPointTo(ballLastHitter != Racket.Owner.Player ? Racket.Owner.Player : ballLastHitter);

        ScoreUpdated();
    }

    private void ScoreUpdated(){
        foreach(var handler in _scoreHandlers)
            handler.ScoreUpdated();
    }
}

public enum RoundEndReason{
    BallHitNet,
    BallLeftTheGamingArea,
    UnsuccessfulServe,
}