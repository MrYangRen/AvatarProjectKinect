using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;



public class Leader_ReturnToPathState : State<Leader>
{
    #region 成员变量
    private Vector3 lastPosition;
    private Entrance entrance;


    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        //MessageDispatcher.Instance().DisparcherMessage(0.0, leader.ID(), MessageEnum.LeaderReturned);
        leader.GetComponent<Vehicle>().enabled = true;
        leader.CleanWaittime();
        lastPosition = leader.States.InteractiveState.GetLastPosition();
        entrance = leader.States.InteractiveState.GetEntrance();
    }

    public override void Execute(Leader leader)
    {
        if (ReturnToPath(leader))
        {
            RevertToEntrance(leader);
        }
    }

    public override void Exit(Leader leader)
    {
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), MessageEnum.LeaderReturned);
        leader.GetComponent<Vehicle>().enabled = false;
        leader.gameObject.GetComponent<SteerForPursuit>().enabled = false;
        leader.CleanWaittime();
    }

    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        
        return true;
    }
    #endregion

    #region 内部服务函数

    private bool ReturnToPath(Leader leader)
    {
        if (Vector3.Distance(leader.transform.position, lastPosition) < Const.ARRIVED_DISTANCE)
        {
            leader.GetComponent<Vehicle>().enabled = false;
            leader.GetComponent<SteerForPoint>().enabled = false;
            leader.LeaderAC.Stop();
            return true;
        }
        leader.GetComponent<Vehicle>().enabled = true;
        leader.GetComponent<SteerForPoint>().TargetPoint = lastPosition;
        leader.GetComponent<SteerForPoint>().enabled = true;
        leader.LeaderAC.Move();
        return false;
    }

    private void RevertToEntrance(Leader leader)
    {
        if (entrance == Entrance.FromIdling)
        {
            leader.GetFSM().ChangeState(leader.States.IdleState);
        }
        else if (entrance == Entrance.FromWalking)
        {
            leader.GetFSM().ChangeState(leader.States.WalkingState);
        }
    }

    #endregion

}
