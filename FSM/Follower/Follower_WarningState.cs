using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Follower_WarningState : State<Follower>
{


    #region 成员变量
    private AudioClip WarnAndSay;
    #endregion

    #region override
    public override void Enter(Follower follower)
    {
        follower.CleanWaittime();
        Follower_WarnAndSay(follower);
        follower.FollowerAC.WaveHand();
    }

    public override void Execute(Follower follower)
    {
        follower.PlusWaittime();
        follower.FollowerAC.Walking();
        if (follower.GetWaittime() > Const.FOLLOWER_WARN_TIME)
        {
            Leader leader=GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>();
            MessageDispatcher.Instance().DisparcherMessage(0.0f, follower.ID(), leader.ID(), MessageEnum.PlayerWantsInteractive);
           
        }
    }

    public override void Exit(Follower follower)
    {
        follower.FollowerAC.StopWalking();
        follower.FollowerAC.StopWaveHand();
        follower.CleanWaittime();
    }


    public override bool OnMessage(Follower follower, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderGotoInteractive: follower.GetFSM().ChangeState(follower.States.WalkingState); break;
            default: return false;
        }
        return true;
    }
    #endregion

    #region 内部服务函数
    private void Follower_WarnAndSay(Follower follower)
    {
        float rand = Random.Range(0, 10);
        if ( rand <= 5)
        {
            WarnAndSay = follower.FollowerVoices[AudioClipHash.Follower_WarnAndSay01];
        }
        else
        {
            WarnAndSay = follower.FollowerVoices[AudioClipHash.Follower_WarnAndSay02];
        }

        follower.voice.clip = WarnAndSay;
        follower.voice.Play();
    }
    #endregion
}