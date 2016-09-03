using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
using System.Collections.Generic;
public class Leader_WalkingState : State<Leader>
{

    #region 成员变量
    private Vector3 ForwardWhenExit;
    private Vector3 PositionWhenExit;
    private float AudioLength;
    private bool TalkSwitch = true;
    private float waveHandTime = 0.0f;
    private bool isTalking = false;

    #endregion


    #region override
    public override void Enter(Leader leader)
    {
        //一进到Walking状态，就开始沿着指定路径行走,并通知其他人也要开始走
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), MessageEnum.StartWalking);
        leader.gameObject.GetComponent<PathFollowingController>().enabled = true;
        leader.gameObject.GetComponent<SteerForPoint>().enabled = true;
        leader.gameObject.GetComponent<Vehicle>().enabled = true;
        leader.CleanWaittime();
    }

    public override void Execute(Leader leader)
    {
        //在走路的时候，随机地以40%(还要继续调试）的概率进入说话的状态
        //而这个检测的更新频率不能和Update一样，而是应该要慢一些.由talk_Frequency定义
        leader.LeaderAC.Move();
        leader.PlusWaittime();
        if(TalkSwitch)
        {
            if (leader.GetWaittime() > Const.LEADER_TALK_FREQUENCY)
            {
                float rand = Random.Range(0, 10);
                if (rand < Const.LEADER_POSSIBILITI_TO_TALK * 10)
                {
                    float rand2 = Random.Range(0, 10);
                    if (rand2 > 4)
                    {
                        Leader_Say(leader, AudioClipHash.Leader_WalkAndSay01);
                    }
                    else if (rand2 > 1)
                    {
                        Leader_Say(leader, AudioClipHash.Leader_WalkAndSay02);
                    }
                    else
                    {
                        Leader_Say(leader, AudioClipHash.Leader_WalkAndSay03);
                    }
                    isTalking = true;
                    if (rand2 < 2.0)
                    {
                        leader.LeaderAC.WaveHand();
                    }
                    else
                    {
                        leader.LeaderAC.MaskWaveHand();
                    }
                   
                }
                leader.CleanWaittime();
            }
            if (isTalking)
            {
                waveHandTime += Time.deltaTime;
                if (waveHandTime > Const.LEADER_WAVEHAND_TIME)
                { 
                    leader.LeaderAC.StopWaveHand();
                    leader.LeaderAC.StopMaskWaveHand();
                    waveHandTime = 0.0f;
                    isTalking = false;
                }
            }
           



        }
    }

    public override void Exit(Leader leader)
    {
        leader.gameObject.GetComponent<Vehicle>().enabled = false;
        leader.gameObject.GetComponent<PathFollowingController>().enabled = false;
        leader.gameObject.GetComponent<SteerForPoint>().enabled = false;
        leader.LeaderAC.Stop();
        leader.LeaderAC.StopWaveHand();
        leader.LeaderAC.StopMaskWaveHand();
        leader.CleanWaittime();
        ForwardWhenExit = leader.gameObject.transform.forward;
        PositionWhenExit = leader.gameObject.transform.position;
    }

    //由于玩家进来的时候主席是就不会随机说话了，所以消息处理里面可以不用考虑当说话的时候收到玩家交互请求
    public override bool OnMessage(Leader leader, MessageEnum msg)
    {

        switch (msg)
        {
            case MessageEnum.StopWalking: leader.GetFSM().ChangeState(leader.States.IdleState); break;
            case MessageEnum.PlayerWantsInteractive:
                {
                    leader.States.InteractiveState.SetEntrance(Entrance.FromWalking);
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

    public void TalkSwitchOn()
    { TalkSwitch = true; }

    public void TalkSwitchOff()
    { TalkSwitch = false; }

    #endregion

    #region 内部服务函数
    private void Leader_Say(Leader leader, string clip)
    {
        leader.voice.clip = leader.LeaderVoices[clip];
        leader.voice.Play();
        AudioLength = leader.voice.clip.length;
    }
    #endregion
}
