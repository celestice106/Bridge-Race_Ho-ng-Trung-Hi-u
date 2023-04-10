using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Bot bot)
    {
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Bot bot)
    {
        bot.BuildBridge();
        if (bot.brickAmount == 0)
        {
            bot.ChangeState(new IdleState());
            timer += Time.deltaTime;
        }
        
        if(timer>= randomTime)
        {
            bot.ChangeState(new FindingState());
        }
    }
    
    public void OnExit(Bot bot)
    {

    }
}
