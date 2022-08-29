using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotState : ScriptableObject
{
    public bool IsInitialized { get; protected set; }

    [field:SerializeField] [field:Range(0, 100f)] protected float RacketMovementSpeed { get; private set; }

    protected Bot Bot;
    protected BotStateMachine StateMachine;
    protected Ball Ball;
    
    protected float StartTime;

    public void Initialize(Bot bot, Ball ball, BotStateMachine stateMachine){
        StateMachine = stateMachine;
        Ball = ball;
        Bot = bot;

        IsInitialized = true;
    }

    public virtual void Enter(){
        StartTime = Time.time;
        Bot.Racket.SetMovementSpeed(RacketMovementSpeed);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit() { }
}
