using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;



public class Player_IdleState : State<Player>
{


    #region 成员变量
    private Vector3 ForwardWhenExit;
    private bool rotateFinished;
    #endregion

    #region override
    public override void Enter(Player player)
    {
        player.CleanWaittime();
        player.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        player.gameObject.GetComponent<SteerForNeighborGroup>().enabled = false;
        player.PlayerAC.Stop();
        //player.PlayerAC.TurnOnSpot(player.target.transform.position - player.gameObject.transform.position);
    }

    public override void Execute(Player player)
    {
        player.PlayerAC.Stop();

        //如果主席在走，玩家一进来也要跟着主席走，如果主席在演讲，就到Listening状态
        if (player.target.GetComponent<Leader>().GetFSM().CurrentState() == player.target.GetComponent<Leader>().States.WalkingState)
        {
            player.GetFSM().ChangeState(player.States.WalkingState);
        }
        if (player.target.GetComponent<Leader>().GetFSM().CurrentState() == player.target.GetComponent<Leader>().States.SpeechState)
        {
            player.GetFSM().ChangeState(player.States.ListeningState);
        }


        //设置相应的交互类型
        if (player.GetInteractiveSwitch())
        {
            if (GUItest.playerWaveHands)
            {
                player.States.WaveHandState.SetEntrance(Entrance.FromIdling);
                player.GetFSM().ChangeState(player.States.WaveHandState);
            }
            if (GUItest.playerSalute)
            {
                player.States.SaluteState.SetEntrance(Entrance.FromIdling);
                player.GetFSM().ChangeState(player.States.SaluteState);
            }
            if (GUItest.playerWalk)
            {
                player.GetFSM().ChangeState(player.States.WalkingState);
            }
        }
    }
        

    public override void Exit(Player player)
    {
        ForwardWhenExit = player.gameObject.transform.forward;
        player.CleanWaittime();
    }

    public override bool OnMessage(Player player, MessageEnum msg)
    {
        switch (msg)
        {
            //case MessageEnum.StartWalking: player.GetFSM().ChangeState(player.States.WalkingState); break;
            case MessageEnum.StartSpeech: player.GetFSM().ChangeState(player.States.ListeningState); break;
        }
        return true;
    }

    #endregion

    #region 外部接口
    public Vector3 GetForwardWhenExit()
    {

        return ForwardWhenExit;
    }
    #endregion



}
