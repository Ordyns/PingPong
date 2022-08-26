using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event System.Action BallCollided;
    public event System.Action<Racket.Owner> BallHittedByRacket;
    public event System.Action BallEnteredIntoDeathZone;

    public Racket.Owner LastHitter { get; private set; }
    public State BallState { get; private set; }

    public Vector3 RigidbodyPosition => _rigidbody.position;

    [SerializeField] private TrailRenderer trail;
    [Space]
    [SerializeField] [Range(0, 100f)] private float movementSpeedDuringServe = 5;

    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        BallCollided?.Invoke();
    }

    public void ResetBall(){
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        
        _rigidbody.MovePosition(new Vector3(0, 1, 0));
        transform.position = new Vector3(0, 1, 0);

        ClearTrail();
    }

    public void PrepareToServe(){
        ResetBall();
        BallState = State.WaitingForServe;
    }

    public void PrepareToHitByRacket(Racket.Owner racketOwner){
        if(BallState == State.WaitingForServe)
            BallState = State.Serving;

        BallHittedByRacket?.Invoke(racketOwner);

        LastHitter = racketOwner;
        _rigidbody.isKinematic = false;

        ClearTrail();
    }

    public void DeathZoneTriggered(){
        BallState = State.Unplayable;

        BallEnteredIntoDeathZone?.Invoke();
    }

    public void ClearTrail() => trail.Clear();

    public void SetVelocity(Vector3 velocity) => _rigidbody.velocity = velocity;
    public void SetRigidbodyPosition(Vector3 position) => _rigidbody.position = position;

    public void MoveSmoothlyTo(Vector3 position){
        if(BallState != State.WaitingForServe)
            return;

        _rigidbody.position = Vector3.Lerp(_rigidbody.position, position, movementSpeedDuringServe * Time.deltaTime);
    }

    public enum State{
        Default,
        Unplayable,
        WaitingForServe,
        Serving
    }
}
