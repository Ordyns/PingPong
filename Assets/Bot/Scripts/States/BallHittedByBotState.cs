using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BallHittedByBotState), menuName = "Bot/" + nameof(BallHittedByBotState))]
public class BallHittedByBotState : BotState
{
    [Header("Start position offset")]
    [SerializeField] private Vector3 minStartPositionOffset;
    [SerializeField] private Vector3 maxStartPositionOffset;

    private Vector3 _racketTargetPosition;

    public override void Enter(){
        base.Enter();

        Vector3 offset = VectorExtensions.RandomRange(minStartPositionOffset, maxStartPositionOffset);
        _racketTargetPosition = Bot.Racket.StartPosition + offset;
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
    }

    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

        Bot.MoveRacketTo(_racketTargetPosition);
    }

    public override void Exit(){
        base.Exit();


    }
}
