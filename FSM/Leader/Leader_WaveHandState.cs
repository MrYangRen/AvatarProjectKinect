using UnityEngine;
using System.Collections;

//保证每一段说话的时长小于挥手时长
public class Leader_WaveHandState : State<Leader>
{

    #region 成员变量
    private AudioClip WaveHandsAndSay; 

    
    #endregion

    #region override
    public override void Enter(Leader leader)
    {
       // Debug.LogWarning(Time.time + "enter wave hand");
        leader.LeaderAC.Stop();
        leader.CleanWaittime();
        leader.LeaderAC.MaskWaveHand();
        Leader_WaveHandAndSay(leader);

    }

    public override void Execute(Leader leader)
    {
        leader.LeaderAC.Stop();
        leader.PlusWaittime();
        if (leader.GetWaittime() > Const.LEADER_WAVEHAND_TIME)
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
        leader.LeaderAC.StopMaskWaveHand();
    }


    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        //不处理任何消息
        switch (msg)
        {
            case MessageEnum.PlayerShakeHands: MessageDispatcher.Instance().DisparcherMessage
                (Const.LEADER_WAVEHAND_TIME, leader.ID(), leader.target.GetComponent<Player>().ID(), MessageEnum.PlayerShakeHands); break;
            default: return false;
        }
        return true;
    }
    #endregion


    #region 内部服务函数
    private void Leader_WaveHandAndSay(Leader leader)
    {
        float rand = Random.Range(0, 10);
        if (rand > 0 && rand <= 3)
        {
            WaveHandsAndSay = leader.LeaderVoices[AudioClipHash.Leader_WaveHandsAndSay01];
        }
        else if (rand > 3 && rand < 6)
        {
            WaveHandsAndSay = leader.LeaderVoices[AudioClipHash.Leader_WaveHandsAndSay02];
        }
        else
        {
            WaveHandsAndSay = leader.LeaderVoices[AudioClipHash.Leader_WaveHandsAndSay03];
        }
        leader.voice.clip = WaveHandsAndSay;
        leader.voice.Play();
    }
    #endregion

}
