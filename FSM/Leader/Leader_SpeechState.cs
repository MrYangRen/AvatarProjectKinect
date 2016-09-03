using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;


//只有Idle状态才会进入，结束之后一定会进入Walking状态，也有可能进入交互状态
public class Leader_SpeechState : State<Leader> {
    

    #region 成员变量
    private float AudioLength = 0.0f;
    //进入这个状态的时候要判断主席是随意说话还是说特殊的话
    private string SpeechContent;
    public GameObject MemoryCamera;
    private bool hasSet = false;
    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        leader.CleanWaittime();
        //这个通过传递消息的时候来设置。发送startSpeech消息之前，通过调用set方法来设置演讲的内容。由地点触发器来发送消息
        Leader_Say(leader,SpeechContent);
        leader.LeaderAC.WaveHand();
        hasSet = false;
        //switch (SpeechContent)
        //{
        //    case AudioClipHash.Leader_SpecialScene01:TipAnimation.SetShowLeaderSayCaption1(true);break;
        //    case AudioClipHash.Leader_SpecialScene02: TipAnimation.SetShowLeaderSayCaption2(true); break;
        //}
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), MessageEnum.StartSpeech);
        
    }

    public override void Execute(Leader leader)
    {
        //播放完声音之后，反转回原来的状态
        leader.PlusWaittime();
        if (leader.GetWaittime() > 5.0f && !hasSet && SpeechContent==AudioClipHash.Leader_SpecialScene01)
        {
            MemoryCamera.gameObject.SetActive(true);
            hasSet = true;
        }
        if (leader.GetWaittime() > AudioLength+1)
        {
            leader.GetFSM().ChangeState(leader.States.AfterSpeechState);
        }
    }

    public override void Exit(Leader leader)
    {
        leader.CleanWaittime();
        leader.voice.Stop();
        leader.LeaderAC.StopWaveHand();
        BGM.Instance().FadeInBGM();
    }
        

    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        return true;
    }

    #endregion

    #region 外部接口
    //每一个演讲地点的触发器发送消息的同时设置
    public void setSpeechContent(string speechContent)
    {
        SpeechContent = speechContent;
    }
    #endregion

    #region 内部服务函数
    private void Leader_Say(Leader leader,string clip)
    {
        leader.voice.clip = leader.LeaderVoices[clip];
        leader.voice.Play();
        AudioLength = leader.voice.clip.length;
    }
    #endregion




}
