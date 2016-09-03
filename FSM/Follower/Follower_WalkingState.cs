using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Follower_WalkingState : State<Follower>
{
    #region override
    public override void Enter(Follower follower)
    {
        follower.CleanWaittime();
        
    }

    public override void Execute(Follower follower)
    {
        follower.FollowerAC.Walking();

    }

    public override void Exit(Follower follower)
    {
        follower.CleanWaittime();
        follower.FollowerAC.StopWalking();
    }


    public override bool OnMessage(Follower follower, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StopWalking: follower.GetFSM().ChangeState(follower.States.IdleState); break;
            case MessageEnum.PlayerWantsInteractive: follower.GetFSM().ChangeState(follower.States.WarningState); break;
            default: return false;
        }
        return true;
    }
    #endregion
}