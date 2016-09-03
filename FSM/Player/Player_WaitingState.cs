using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

//bug:玩家回到Idle状态之后，就出现了不正常的形态，脚一直在动

//等待状态
public class Player_WaitingState : State<Player>
{
    #region 成员变量
    private Entrance entrance;
    #endregion
    #region override
    public override void Enter(Player player)
    {
        player.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        player.CleanWaittime();
        player.PlayerAC.Stop();
    }

    public override void Execute(Player player)
    {
        //player.PlayerAC.Stop();
        player.transform.LookAt(player.target.transform.position);


        if (GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<MyCamera>().getPlotSwitch())
        {
            player.GetFSM().ChangeState(player.States.WalkingState);
        }

        float TimeDelayed = 0.0f;
        if (player.target.GetComponent<Leader>().GetFSM().CurrentState() == player.target.GetComponent<Leader>().States.WaitingState)
        {
            if (player.target.GetComponent<Leader>().States.WaitingState.GetHasWaitedTime() < Const.LEADER_REACT_TIME)
            {
                TimeDelayed = player.target.GetComponent<Leader>().States.WaitingState.GetHasWaitedTime();
            }
            else
            {
                TimeDelayed=Const.LEADER_REACT_TIME;
            }
        }

        if (GUItest.playerWaveHands)
        {
            MessageDispatcher.Instance().DisparcherMessage(TimeDelayed, player.ID(), player.target.GetComponent<Leader>().ID(), MessageEnum.PlayerWaveHands);
            player.GetFSM().ChangeState(player.States.WaveHandState);
        }
        else if (GUItest.playerSalute)
        {
            MessageDispatcher.Instance().DisparcherMessage(TimeDelayed, player.ID(), player.target.GetComponent<Leader>().ID(), MessageEnum.PlayerSalute);
            player.GetFSM().ChangeState(player.States.SaluteState);
        }
        if (Vector3.Distance(player.gameObject.transform.position, player.target.transform.position) < Const.INTERACTIVE_DISTANCE)
        {
            TipAnimation.SetPlayerCanShakeHands(true);

            if (GUItest.playerShakeHands)
            {
                //给主席发消息
                MessageDispatcher.Instance().DisparcherMessage(0.5f, player.ID(), player.target.GetComponent<Leader>().ID(), MessageEnum.PlayerShakeHands);
                player.GetFSM().ChangeState(player.States.ShakeHandsState);
            }
        }
        else { TipAnimation.SetPlayerCanShakeHands(false); }

    }

    public override void Exit(Player player)
    {
        TipAnimation.SetPlayerCanShakeHands(false);
        player.CleanWaittime();
    }

    public override bool OnMessage(Player player, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderReturned: RevertToEntrance(player); break;
                //主席靠近玩家，玩家也要靠近主席
            case MessageEnum.LeaderCloseToPlayer: player.GetFSM().ChangeState(player.States.CloseToLeaderState); break;
        }
        return true;
    }

    #endregion

    #region 外部接口
    public void SetEntrance(Entrance en)
    { entrance = en; }
    #endregion

    #region 内部服务函数
    private void RevertToEntrance(Player player)
    {
        if (entrance == Entrance.FromIdling)
        {
            player.GetFSM().ChangeState(player.States.IdleState);
        }
        else if (entrance == Entrance.FromWalking)
        {
            player.GetFSM().ChangeState(player.States.WalkingState);
        }
    }
    #endregion


}
