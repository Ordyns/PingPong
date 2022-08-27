using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketTrigger : MonoBehaviour
{
    public event System.Action<Ball> BallTriggered;
    
    [SerializeField] private Racket racket;
    [SerializeField] private Transform aimTarget;
    
    [Header("> Force")]
    [SerializeField] private float force = 13;
    [SerializeField] private float verticalForce = 7;

    [Header("> Serve force")]
    [SerializeField] private float serveForce = 13;
    [SerializeField] private float verticalServeForce = 7;

    [Header("> Velocity")]
    [SerializeField] private float velocityStrength = 1;
    [SerializeField] private Vector3 maxVelocity = new Vector3(3, 3, 3);

    [Header("> Ball")]
    [SerializeField] private bool ballPassingCompensation;
    [SerializeField] private float ballPassingThreshold;
    [SerializeField] private float distanceFromBallToRacketAfterPassing = 0.1f;

    private void OnTriggerEnter(Collider other) => OnTrigger(other);
    private void OnTriggerStay(Collider other) => OnTrigger(other);
    private void OnTriggerExit(Collider other) => OnTrigger(other);

    private void OnTrigger(Collider other){
        if(other.TryGetComponent<Ball>(out Ball ball)){
            ball.PrepareToHitByRacket(racket.RacketOwner);

            SetBallVelocity(ball);
            CheckBallPassing(ball);

            BallTriggered?.Invoke(ball);
        }
    }

    private void SetBallVelocity(Ball ball){
        bool isServing = ball.BallState == Ball.State.Serving || ball.BallState == Ball.State.WaitingForServe;

        Vector3 direction = aimTarget.position - transform.position;
        float velocity = Mathf.Clamp(racket.Velocity.x * velocityStrength, -maxVelocity.x, maxVelocity.x);
        float ballForce = isServing ? serveForce : force;
        float ballVerticalForce = isServing ? verticalServeForce : verticalForce;

        ball.SetVelocity(direction.normalized * ballForce + new Vector3(velocity, ballVerticalForce, 0));
    }

    private void CheckBallPassing(Ball ball){
        if(ballPassingCompensation){
            if(racket.transform.position.z - ball.transform.position.z > ballPassingThreshold){
                Vector3 newPosition = new Vector3(){
                    x = ball.RigidbodyPosition.x,
                    y = ball.RigidbodyPosition.y,
                    z = racket.RigidbodyPosition.z + distanceFromBallToRacketAfterPassing
                };

                ball.SetRigidbodyPosition(newPosition);
            }
        }
    }
}
