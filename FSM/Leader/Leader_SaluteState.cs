using UnityEngine;
using System.Collections;

//保证每一段说话的时长小于敬礼时长
public class Leader_SaluteState : State<Leader>
{

    #region 成员变量
    private AudioClip SaluteAndSay;
    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        leader.LeaderAC.Stop();
        leader.CleanWaittime();
        leader.LeaderAC.Salute();
        Leader_SaluteAndSay(leader);

    }

    public override void Execute(Leader leader)
    {
        leader.LeaderAC.Stop();
        leader.PlusWaittime();
        if (leader.GetWaittime() > Const.LEADER_SALUTE_TIME)
        {
            //如果是原地交互，就回到interactive状态转回去
            if (!leader.States.InteractiveState.GetNeedApproch())
            {
                leader.States.InteractiveState.SetInteractiveType(InteractiveType.none);
                leader.GetFSM().ChangeState(leader.States.InteractiveState);
            }
            else if (leader.GetFSM().PreviousState() == leader.States.WaitingState)
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
        leader.LeaderAC.StopSalute();
    }


    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        //不处理任何消息
        switch (msg)
        {
            case MessageEnum.PlayerShakeHands: MessageDispatcher.Instance().DisparcherMessage
                (Const.LEADER_SALUTE_TIME, leader.ID(), leader.target.GetComponent<Player>().ID(), MessageEnum.PlayerShakeHands); break;
            default: return false;
        }
        return true;
    }
    #endregion


    #region 内部服务函数
    private void Leader_SaluteAndSay(Leader leader)
    {
        float rand = Random.Range(0, 10);
        if (rand > 0 && rand <= 3)
        {
            SaluteAndSay = leader.LeaderVoices[AudioClipHash.Leader_SaluteAndSay01];
        }
        else if (rand > 3 && rand < 6)
        {
            SaluteAndSay = leader.LeaderVoices[AudioClipHash.Leader_SaluteAndSay02];
        }
        else
        {
            SaluteAndSay = leader.LeaderVoices[AudioClipHash.Leader_SaluteAndSay03];
        }
        leader.voice.clip = SaluteAndSay;
        leader.voice.Play();
    }
    #endregion

}

