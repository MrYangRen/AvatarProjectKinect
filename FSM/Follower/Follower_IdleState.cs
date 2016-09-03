using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Follower_IdleState : State<Follower>
{

    #region override
    public override void Enter(Follower follower)
    {
        follower.CleanWaittime();
        follower.FollowerAC.StopWalking();
        
        //Debug.Log("follower进入了IdleState!");
    }

    public override void Execute(Follower follower)
    {
        follower.FollowerAC.StopWalking();

    }

    public override void Exit(Follower follower)
    {
        follower.CleanWaittime();
    }


    public override bool OnMessage(Follower follower, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StartWalking: follower.GetFSM().ChangeState( follower.States.WalkingState); break;
            case MessageEnum.PlayerWantsInteractive: follower.GetFSM().ChangeState(follower.States.WarningState); break;
            case MessageEnum.LeaderGotoInteractive: follower.GetFSM().ChangeState(follower.States.WalkingState); break;
            case MessageEnum.StartSpeech: follower.GetFSM().ChangeState(follower.States.ListeningState); break;
            default: return false;
        }
        return true;
    }
    #endregion

}
