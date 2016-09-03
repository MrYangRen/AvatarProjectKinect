using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Follower_HandClapingState : State<Follower>
{

    #region override
    public override void Enter(Follower follower)
    {
        follower.CleanWaittime();
        follower.FollowerAC.ClapHands();
    }

    public override void Execute(Follower follower)
    {
        //Debug.Log("follower 进入鼓掌状态");
        follower.PlusWaittime();
        if (follower.GetWaittime() > Const.FOLLOWER_CLAPHANDS_TIME)
        {
            follower.GetFSM().ChangeState(follower.States.IdleState);
        }
    }

    public override void Exit(Follower follower)
    {
        follower.FollowerAC.StopClapHands();
        follower.CleanWaittime();
    }


    public override bool OnMessage(Follower follower, MessageEnum msg)
    {
        switch (msg)
        {
            default: return false;
        }
        return true;
    }
    #endregion
}
