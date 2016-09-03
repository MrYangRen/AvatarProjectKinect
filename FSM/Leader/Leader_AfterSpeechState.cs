using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Leader_AfterSpeechState : State<Leader>
{

    #region 成员变量
    private Vector3 ForwardWhenExit;
    private Vector3 PositionWhenExit;
    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        MessageDispatcher.Instance().DisparcherMessage(0.0, leader.ID(), MessageEnum.LeaderStopSpeech);
        leader.gameObject.GetComponent<PathFollowingController>().enabled = false;
        leader.gameObject.GetComponent<SteerForPoint>().enabled = false;
        leader.LeaderAC.Stop();
        leader.CleanWaittime();
    }

    public override void Execute(Leader leader)
    {
        leader.PlusWaittime();
        leader.LeaderAC.Stop();
        if( leader.GetWaittime() > Const.LEADER_WAIT_TIME_AFTER_SPEECH)
        {
            leader.GetFSM().ChangeState(leader.States.WalkingState);
        }
    }

    public override void Exit(Leader leader)
    {
        leader.CleanWaittime();
        PositionWhenExit = leader.gameObject.transform.position;
        ForwardWhenExit = leader.gameObject.transform.forward;
    }


    
    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        switch (msg)
        {
            default: return false;
        }
        return true;
    }
    #endregion

}
