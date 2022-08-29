using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeManager : MonoBehaviour
{
    public event System.Action<Racket.Owner> NewServeStarted;
    public event System.Action<Racket.Owner> UnsuccessfulServe;

    [field:SerializeField] public Ball Ball { get; private set; }
    [Space]
    [SerializeField] private Racket firstPlayer;
    [SerializeField] private Racket secondPlayer;
    [Space]
    [SerializeField] private float delayBeforeNextServe;

    public bool IsSomeoneServing { get; private set; }

    public Racket.Owner CurrentServeOwner { get; private set; }
    private Racket _currentRacket => CurrentServeOwner == firstPlayer.RacketOwner ? firstPlayer : secondPlayer;

    private int _servesCount;
    private bool _overtime;

    private void Start() {
        Ball.BallCollided += OnBallCollided;
        Ball.BallHittedByRacket += OnBallHittedByRacket;
        Ball.BallEnteredIntoDeathZone += (zoneType) => StartCoroutine(WaitForDelayBeforeServe());
        UnsuccessfulServe += (serveOwner) => StartCoroutine(WaitForDelayBeforeServe());
        NextServe();
    }

    private IEnumerator WaitForDelayBeforeServe(){
        yield return new WaitForSecondsRealtime(delayBeforeNextServe);
        NextServe();
    }

    private void NextServe(){
        int servesToAlternateServe = _overtime ? 1 : 2;

        if(_servesCount == servesToAlternateServe){
            _servesCount = 1;
            CurrentServeOwner = (CurrentServeOwner == firstPlayer.RacketOwner) ? secondPlayer.RacketOwner : firstPlayer.RacketOwner;
        }
        else{
            _servesCount++;
        }
        
        IsSomeoneServing = true;
        Ball.PrepareToServe();

        NewServeStarted?.Invoke(CurrentServeOwner);
    }

    private void FixedUpdate() {
        if(Ball.BallState == Ball.State.WaitingForServe){
            float xPosition = Mathf.Lerp(-_currentRacket.BallServePositionRange / 2, _currentRacket.BallServePositionRange / 2, _currentRacket.GetMovemenetProgress());
            Ball.MoveSmoothlyTo(new Vector3(xPosition, _currentRacket.ServePosition.y, _currentRacket.ServePosition.z));
        }
    }

    private void OnBallHittedByRacket(Racket.Owner racketOwner){
        if(Ball.BallState == Ball.State.Serving && IsSomeoneServing && racketOwner != CurrentServeOwner){
            bool isSuccessfullyServed = IsServeTechnicallyCorrect();
            
            if(isSuccessfullyServed)
                Ball.SuccessfullyServed();
            else
                UnsuccessfulServe?.Invoke(racketOwner);

            IsSomeoneServing = false;
        }
    }

    private void OnBallCollided(){
        if(Ball.BallState == Ball.State.Serving && IsSomeoneServing){
            var hittedZonesOwners = Ball.HittedTableZonesOwners;
            if(hittedZonesOwners.Count == 1 && hittedZonesOwners.Contains(Ball.LastHitter) == false)
                UnsuccessfulServe?.Invoke(CurrentServeOwner);
        }
    }
        
    public bool IsServeTechnicallyCorrect() => Ball.HittedTableZonesOwners.Count == 2;
}