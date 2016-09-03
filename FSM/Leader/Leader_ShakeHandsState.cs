using UnityEngine;
using System.Collections;


//保证每一段说话的时长小于敬礼时长
public class Leader_ShakeHandsState : State<Leader>
{

    #region 成员变量
    private AudioClip ShakeHandsAndSay;
    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        leader.LeaderAC.Stop();
        leader.CleanWaittime();
        leader.LeaderAC.ShakeHands();
        Leader_ShakeHandsAndSay(leader);

    }

    public override void Execute(Leader leader)
    {
        leader.LeaderAC.Stop();
        leader.PlusWaittime();
        if (leader.GetWaittime() > Const.LEADER_SHAKEHANDS_TIME)
        {
            if (leader.GetFSM().PreviousState() == leader.States.WaitingState)
            {
                leader.States.WaitingState.SetNeedReturn(true);
                leader.GetFSM().ChangeState(leader.States.WaitingState);
            }
            else
            {
                leader.GetFSM().ChangeState(leader.States.ReturnToPathState);
            }
        }
    }

    public override void Exit(Leader leader)
    {
        leader.CleanWaittime();
        leader.LeaderAC.StopShakeHands();
    }


    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        //不处理任何消息
        switch (msg)
        {
            case MessageEnum.PlayerShakeHands: MessageDispatcher.Instance().DisparcherMessage
                (Const.LEADER_SHAKEHANDS_TIME, leader.ID(), leader.target.GetComponent<Player>().ID(), MessageEnum.PlayerShakeHands); break;
            case MessageEnum.PlayerStopShakeHands: leader.GetFSM().ChangeState(leader.States.WaitingState); break;
            default: return false;
        }
        return true;
    }
    #endregion


    #region 内部服务函数
    private void Leader_ShakeHandsAndSay(Leader leader)
    {
        float rand = Random.Range(0, 10);
        if (rand <= 5)
        {
            ShakeHandsAndSay = leader.LeaderVoices[AudioClipHash.Leader_ShakeHandsAndSay01];
        }
        else
        {
            ShakeHandsAndSay = leader.LeaderVoices[AudioClipHash.Leader_ShakeHandsAndSay02];
        }

        leader.voice.clip = ShakeHandsAndSay;
        leader.voice.Play();
    }
    #endregion

}