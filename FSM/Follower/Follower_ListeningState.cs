using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Follower_ListeningState : State<Follower>
{


    #region 成员变量
    private bool rotateFinished = false;
    #endregion 

    #region override
    public override void Enter(Follower follower)
    {
        rotateFinished = false;
        follower.CleanWaittime();
        follower.FollowerAC.StopWalking();
       // Debug.Log("follower进入了ListeningState!");
    }

    public override void Execute(Follower follower)
    {
        //if (!rotateFinished)
        //{
        //    rotateFinished = follower.FollowerAC.TurnOnSpot(follower.target.transform.position - follower.gameObject.transform.position);
        //}
        follower.gameObject.transform.LookAt(follower.target.transform.position);
        follower.FollowerAC.StopWalking();
       // Debug.Log("follower执行ListeningState!");

    }

    public override void Exit(Follower follower)
    {
        follower.CleanWaittime();
        //Debug.Log("follower退出了ListeningState!");
    }


    public override bool OnMessage(Follower follower, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderStopSpeech: follower.GetFSM().ChangeState(follower.States.HandClapingState); break;
            default: return false;
        }
        return true;
    }
    #endregion
}
