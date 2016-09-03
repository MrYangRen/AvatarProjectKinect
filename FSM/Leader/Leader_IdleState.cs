using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Leader_IdleState : State<Leader>
{

    #region 成员变量
    private Vector3 ForwardWhenExit;
    private Vector3 PositionWhenExit;
    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), MessageEnum.StopWalking);
        leader.gameObject.GetComponent<PathFollowingController>().enabled = false;
        leader.gameObject.GetComponent<SteerForPoint>().enabled = false;
        leader.gameObject.GetComponent<Vehicle>().enabled = false;
        leader.LeaderAC.Stop();
        leader.CleanWaittime();
    }

    public override void Execute(Leader leader)
    {
        leader.LeaderAC.Stop();
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
            case MessageEnum.StartWalking: leader.GetFSM().ChangeState(leader.States.WalkingState); break;
            case MessageEnum.StartSpeech:
            {
                leader.GetFSM().ChangeState(leader.States.SpeechState);
                break;
            }
            case MessageEnum.PlayerWantsInteractive:
            {
                leader.States.InteractiveState.SetEntrance(Entrance.FromIdling);
                leader.GetFSM().ChangeState(leader.States.InteractiveState);
                break;
            }
            default: return false;
        }
        return true;
    }
    #endregion

    #region 外部接口
    public Vector3 GetForwardWhenExit()
    {
        return ForwardWhenExit;
    }
    public Vector3 GetPositionWhenExit()
    {
        return PositionWhenExit;
    }
    #endregion
}
