using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public float timer;
    public float randomTime;
   public void OnEnter(Bot bot)
    {
        timer = 0;
        randomTime = Random.Range(1f, 3f);
    }
    public void OnExecute(Bot bot)
    {
        bot.StopMoving();
        timer += Time.deltaTime;
        if(timer >= randomTime)
        {
            bot.ChangeState(new FindingState());
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
