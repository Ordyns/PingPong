using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public event System.Action BallCollided;
    public event System.Action<Racket.Owner> BallHittedByRacket;
    public event System.Action<BallDeathZone.ZoneType> BallEnteredIntoDeathZone;

    public Racket.Owner LastHitter { get; private set; }
    public State BallState { get; private set; }

    public Vector3 RigidbodyPosition => _rigidbody.position;
    public ReadOnlyCollection<Racket.Owner> HittedTableZonesOwners => _hittedTableZonesOwners.AsReadOnly();

    [SerializeField] [Range(0, 100f)] private float movementSpeedDuringServe = 5;

    [Header("Visual")]
    [SerializeField] private BallEffects ballEffects;
    [Space]
    [SerializeField] private TrailRenderer trail;

    private List<Racket.Owner> _hittedTableZonesOwners = new List<Racket.Owner>();
    private Rigidbody _rigidbody;

    private void OnValidate() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.TryGetComponent<TableZone>(out TableZone tableZone)){
            if(_hittedTableZonesOwners.Contains(tableZone.ZoneOwner) == false)
                _hittedTableZonesOwners.Add(tableZone.ZoneOwner);

            ballEffects.OnCollision();
        }

        BallCollided?.Invoke();
    }

    public void ResetBall(){
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        
        _rigidbody.MovePosition(new Vector3(0, 1, 0));
        transform.position = new Vector3(0, 1, 0);

        _hittedTableZonesOwners = new List<Racket.Owner>();

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
        _hittedTableZonesOwners = new List<Racket.Owner>();

        LastHitter = racketOwner;
        _rigidbody.isKinematic = false;

        ClearTrail();
    }

    public void SuccessfullyServed(){
        if(BallState == State.Serving)
            BallState = State.Default;
    }

    public void DeathZoneTriggered(BallDeathZone.ZoneType zoneType){
        BallEnteredIntoDeathZone?.Invoke(zoneType);
        BallState = State.Unplayable;

        ballEffects.OnDeathCollision();
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
