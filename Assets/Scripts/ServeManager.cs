using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeManager : MonoBehaviour
{
    public event System.Action<Racket.Owner> NewServeStarted;

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
        Ball.BallEnteredIntoDeathZone += () => StartCoroutine(WaitForDelayBeforeServe());
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
}