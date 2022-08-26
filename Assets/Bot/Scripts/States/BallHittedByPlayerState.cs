using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BallHittedByPlayerState), menuName = "Bot/" + nameof(BallHittedByPlayerState))]
public class BallHittedByPlayerState : BotState
{
    public override void Enter(){
        base.Enter();

        Bot.MoveAimTargetToRandomPosition();
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
    }

    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

        Vector3 racketTargetPosition = new Vector3(Ball.RigidbodyPosition.x, Bot.Racket.RigidbodyPosition.y, Bot.Racket.RigidbodyPosition.z);
        Bot.MoveRacketTo(racketTargetPosition);
    }

    public override void Exit(){
        base.Exit();
    }
}
