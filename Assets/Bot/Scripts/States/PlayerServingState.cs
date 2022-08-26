using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerServingState), menuName = "Bot/" + nameof(PlayerServingState))]
public class PlayerServingState : BotState
{
    [SerializeField] private Vector3 minStartPositionOffset;
    [SerializeField] private Vector3 maxStartPositionOffset;
    [Space]
    [SerializeField] [Range(0, 100f)] private float movementSpeed = 5;

    private Vector3 _racketTargetPosition;

    public override void Enter(){
        base.Enter();

        Vector3 offset = VectorExtensions.RandomRange(minStartPositionOffset, maxStartPositionOffset);
        _racketTargetPosition = Bot.Racket.StartPosition + offset;
        Bot.Racket.SetMovementSpeed(movementSpeed);
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
