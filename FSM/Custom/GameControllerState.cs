using UnityEngine;
using System.Collections;

public class GameControllerState : State<GameController> {
    private bool alreadyHasPlayer=false;
    private Massage_Enum playerState;


    #region override
    public override void Enter(GameController Onwer)
    {
        DontShowPlayer(Owner);   
    }

    public override void Execute(GameController Onwer)
    {
        //在这个状态中一直检测，并且给Leader发送消息
        if (GUItest.startWalking)
        {
            MessageDispatcher.Instance().DisparcherMessage(0.0f, Onwer.ID(), Onwer.leader.ID(), MessageEnum.StartWalking);
        }
        if (GUItest.startSpeech)
        {
            Owner.leader.States.SpeechState.setSpeechContent(AudioClipHash.Leader_SpecialScene01);
            MessageDispatcher.Instance().DisparcherMessage(0.0f, Onwer.ID(), Onwer.leader.ID(), MessageEnum.StartSpeech);
        }
        if (GUItest.stopWalking)
        {
            MessageDispatcher.Instance().DisparcherMessage(0.0f, Owner.ID(), Onwer.leader.ID(), MessageEnum.StopWalking);
        }
        //如果场地上还没有玩家，检测到玩家就让玩家出现在一个随机位置
        if (!alreadyHasPlayer)
        {
            Onwer.leader.target = null;
            if (GUItest.playerEnterIn)
            {
                //显示玩家
                StartCoroutine(ShowPlayer(Owner));
                alreadyHasPlayer = true;
            }
            //如果场景当中还没有玩家就打开主席走路时说话的开关
            Owner.leader.States.WalkingState.TalkSwitchOn();
        }
        else
        {
            //如果场景当中有玩家就打开关闭主席走路时说话的开关
            Owner.leader.States.WalkingState.TalkSwitchOff();
        }
        if (GUItest.playerExit)
        {
            DontShowPlayer(Owner);
            alreadyHasPlayer = false;
        }
        if (GUItest.playerEnterWrong)
        { alreadyHasPlayer = false; }
    }

    public override void Exit(GameController Onwer)
    {
    }

    public override bool OnMessage(GameController Onwer, MessageEnum msg)
    {
        return true;
    }

    #endregion 

    #region 内部服务函数
    private IEnumerator ShowPlayer(GameController Owner)
    {
        float offset=Random.Range(-8, -5);
        Vector3 newPos = Owner.leader.transform.position + offset*Owner.leader.transform.forward;
        Owner.player.transform.position = newPos;
        while (Owner.GetWaittime() <= 2.0f) { Owner.PlusWaittime() ; yield return 0; }
        Owner.leader.GetComponent<Leader>().SetTarget(GameObject.FindGameObjectWithTag(Tags.Player));
        Transform parentGO = Owner.player.gameObject.transform;
        foreach (Transform obj in parentGO)
        {
            obj.gameObject.SetActive(true);
        }
        Owner.CleanWaittime();
    }

    private void DontShowPlayer(GameController Owner)
    {
        Transform parentGO = Owner.player.gameObject.transform;
        foreach (Transform obj in parentGO)
        {
            obj.gameObject.SetActive(false);
        }
    }
    #endregion

    #region 外部接口
    public bool getAlreadyHasPlayer()
    { return alreadyHasPlayer; }

    #endregion

}
