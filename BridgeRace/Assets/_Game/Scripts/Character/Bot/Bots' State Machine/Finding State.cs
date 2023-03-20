using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Bot bot)
    {
        timer = 0;
        randomTime = Random.Range(8f, 10f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        BotAction.ins.FindBrick();
        if(BotAction.ins.IsDestination)
        {
            if (bot.brickAmount >= GameConstants.MAX_BRICK_CARRIED + Random.Range(0, 2))
                bot.ChangeState(new BuildState());
            else
                BotAction.ins.targetedBrick = null;
        }
        if (timer > randomTime)
        {
            bot.ChangeState(new IdleState());
        } 
    }

    public void OnExit(Bot bot)
    {
        
    }
}
