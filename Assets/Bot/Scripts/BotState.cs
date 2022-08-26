using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotState : ScriptableObject
{
    public bool isInitialized { get; protected set; }

    protected Bot Bot;
    protected BotStateMachine StateMachine;
    protected Ball Ball;
    
    protected float StartTime;

    public void Initialize(Bot bot, Ball ball, BotStateMachine stateMachine){
        StateMachine = stateMachine;
        Ball = ball;
        Bot = bot;

        isInitialized = true;
    }

    public virtual void Enter(){
        StartTime = Time.time;
        Bot.ResetRacketMovementSpeed();
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit() { }
}
