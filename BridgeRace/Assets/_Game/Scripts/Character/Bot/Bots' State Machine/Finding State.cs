using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingState : IState
{

    public void OnEnter(Bot bot)
    {

    }

    public void OnExecute(Bot bot)
    {
        //bot.ChangeAnim(AnimName.RUN);
        bot.FindBrick();
        if (bot.IsDestination)
        {
            if (bot.brickAmount >= GameConstants.MAX_BRICK_CARRIED + Random.Range(1, 3))
            {
                bot.ChangeState(new BuildState());
            }
            bot.targetedBrick = null;
        }

        /*if (bot.brickAmount >= Random.Range(5, 8))
        {
            bot.ChangeState(new IdleState());
        }*/
    }

    public void OnExit(Bot bot)
    {

    }
}
