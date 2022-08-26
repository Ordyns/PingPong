using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStateMachine
{
    public BotState CurrentState { get; private set; }

    private Bot _bot;
    private Ball _ball;

    public BotStateMachine(Bot bot, Ball ball, BotState startState){
        _bot = bot;
        _ball = ball;
        ChangeState(startState);
    }

    public void ChangeState(BotState state){
        if(CurrentState != null){
            if(CurrentState.GetType() == state.GetType())
                return;
            else 
                CurrentState.Exit();
        }
        
        CurrentState = MonoBehaviour.Instantiate(state);
        CurrentState.Initialize(_bot, _ball, this);
        CurrentState.Enter();
    }
}
