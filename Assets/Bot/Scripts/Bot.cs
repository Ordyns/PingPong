using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Racket Racket => racket;

    [Header("> Serves")]
    [SerializeField] private ServeManager serveManager;
    [field:SerializeField] public float MinAbsRacketServePositionX { get; private set; }
    [field:SerializeField] public float MaxAbsRacketServePositionX { get; private set; }
    
    [Header("> Aim target")]
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float minAimTargetPosition = -1.9f;
    [SerializeField] private float maxAimTargetPosition = 1.9f;
    
    [Header("> Racket")]
    [SerializeField] private Racket racket;

    [Header("> Difficulty")]
    [SerializeField] private BotDifficulty difficulty;

    private BotStateMachine _stateMachine;

    private Ball _ball;

    private float _startRacketMovementSpeed;

    private void Start() {
        _ball = serveManager.Ball;
        _startRacketMovementSpeed = racket.MovementSpeed;

        _stateMachine = new BotStateMachine(this, _ball, difficulty.BallHittedByPlayerState);
        serveManager.NewServeStarted += OnNewServeStarted;
    }

    public void SetDifficulty(BotDifficulty difficulty){
        this.difficulty = difficulty;
    }

    private void OnNewServeStarted(Racket.Owner owner){
        _stateMachine.ChangeState(owner == racket.RacketOwner ? difficulty.BotServingState : difficulty.PlayerServingState);
    }

    private void Update() {
        if(_ball.BallState == Ball.State.Default || _ball.BallState == Ball.State.Serving){
            BotState state = _ball.LastHitter == Racket.RacketOwner ? difficulty.BallHittedByBotState : difficulty.BallHittedByPlayerState;
            _stateMachine.ChangeState(state);
        }

        if(_stateMachine.CurrentState.IsInitialized)
            _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() {
        if(_stateMachine.CurrentState.IsInitialized)
            _stateMachine.CurrentState.PhysicsUpdate();
    }

    public void ResetRacketMovementSpeed() => racket.SetMovementSpeed(_startRacketMovementSpeed);

    public void MoveRacketTo(Vector3 position){
        racket.MoveToPosition(position);
        racket.UpdateRotation();
    }

    public void MoveAimTargetToRandomPosition(){
        MoveAimTargetTo(Random.Range(minAimTargetPosition, maxAimTargetPosition));
    }

    public void MoveAimTargetTo(float xPosition){
        aimTarget.position = new Vector3(xPosition, aimTarget.position.y, aimTarget.position.z);
    }
}
