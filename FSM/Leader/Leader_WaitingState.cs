using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Leader_WaitingState : State<Leader>
{
    #region 成员变量
    private bool needReturn = false;
    private float hasWaitedTime = 0.0f;
    #endregion

    #region override

    public override void Enter(Leader leader)
    {
        leader.gameObject.GetComponent<Vehicle>().enabled = false;
        leader.LeaderAC.Stop();
        leader.CleanWaittime();
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), MessageEnum.StopWalking);
        hasWaitedTime = 0.0f;

    }

    public override void Execute(Leader leader)
    {
        leader.LeaderAC.Stop();
        leader.PlusWaittime();
        hasWaitedTime += Time.deltaTime;
        if (leader.GetWaittime() > Const.LEADER_WAIT_TIME && !needReturn)
        {
            switch (leader.States.InteractiveState.GetInteractiveType())
            {
                case InteractiveType.Salute: leader.GetFSM().ChangeState(leader.States.SaluteState); break;
                case InteractiveType.WaveHands: leader.GetFSM().ChangeState(leader.States.WaveHandState); break;
            }
        }
        else if (leader.GetWaittime() > Const.LEADER_WAIT_TIME)
        {
            MessageDispatcher.Instance().DisparcherMessage(0.0f,leader.ID(),GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<MyCamera>().ID(),MessageEnum.LeaderStopInteractive);
            leader.GetFSM().ChangeState(leader.States.ReturnToPathState);
        }
        
    }

    public override void Exit(Leader leader)
    {
        //设置交互类型为完成交互
        leader.CleanWaittime();
    }

    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.PlayerShakeHands: leader.GetFSM().ChangeState(leader.States.ShakeHandsState); break;
            case MessageEnum.PlayerWaveHands: leader.GetFSM().ChangeState(leader.States.WaveHandState); break;
            case MessageEnum.PlayerSalute: leader.GetFSM().ChangeState(leader.States.SaluteState); break;
            default: return false;
        }
        return true;
    }
    #endregion

    #region 外部接口
    public void SetNeedReturn(bool flag)
    { needReturn = flag; }

    public float GetHasWaitedTime()
    { return hasWaitedTime; }
    #endregion

}
