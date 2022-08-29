using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = nameof(BotServingState), menuName = "Bot/" + nameof(BotServingState))]
public class BotServingState : BotState
{
    [Space]
    [SerializeField] private float startDelay = 1f;
    [Space]
    [SerializeField] private Ease movementEase;

    [Header("Serve")]
    [SerializeField] [Range(0.01f, 100f)] private float serveMovementSpeed = 10f;
    [SerializeField] private float delayBeforeServe = 0.25f;
    [SerializeField] private Ease serveMovementEase;

    private Vector3 _racketServePosition;
    
    private bool isRacketAtTargetPosition;
    private bool isServeAllowed;

    // debug
    private float _movemenetDurationToBall;
    private float _time;
    private Vector3 _ballPosition;

    public override void Enter(){
        base.Enter();
        
        Bot.StartCoroutine(WaitForStartDelay());
    }

    private IEnumerator WaitForStartDelay(){
        yield return new WaitForSeconds(startDelay);

        MoveToServePosition();
    }
    
    private void MoveToServePosition(){
        float xPosition = Random.Range(Bot.MinAbsRacketServePositionX, Bot.MaxAbsRacketServePositionX);
        xPosition *= (Random.Range(0, 1f) > 0.5f) ? -1 : 1;
        _racketServePosition = new Vector3(xPosition, Bot.Racket.StartPosition.y, Bot.Racket.StartPosition.z);

        Bot.MoveAimTargetTo(xPosition * -1);

        float duration = Vector3.Distance(Bot.Racket.RigidbodyPosition, _racketServePosition) / RacketMovementSpeed;
        Bot.Racket.transform.DOLocalMove(_racketServePosition, duration).SetEase(movementEase).OnComplete(() => MoveToBall());
    }

    private void MoveToBall(){
        isRacketAtTargetPosition = true;

        float duration = Vector3.Distance(Bot.Racket.RigidbodyPosition, Ball.RigidbodyPosition) / serveMovementSpeed;
        Bot.Racket.transform.DOLocalMove(Ball.RigidbodyPosition, duration).SetEase(serveMovementEase).SetDelay(delayBeforeServe).OnUpdate(() => {
            _time += Time.deltaTime;
        });

        _ballPosition = Ball.RigidbodyPosition;
        _movemenetDurationToBall = duration;
    }

    public override void LogicUpdate() => base.LogicUpdate();
    
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

        if(isRacketAtTargetPosition == false){
            Bot.Racket.UpdateRotation();
        }
    }

    public override void Exit() => base.Exit();
}
