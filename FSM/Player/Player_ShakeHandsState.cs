using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;


public class Player_ShakeHandsState : State<Player>
{

    #region 成员变量
    #endregion

    #region override
    public override void Enter(Player player)
    {
        player.CleanWaittime();
        player.PlayerAC.Stop();
        player.PlayerAC.ShakeHands();
    }

    public override void Execute(Player player)
    {
        player.PlayerAC.Stop();
        player.PlusWaittime();
	    if (/*!GUItest.playerShakeHands ||*/player.GetWaittime()>Const.PLAYER_SHAKEHANDS_TIME)
        {
            MessageDispatcher.Instance().DisparcherMessage(0.0f, player.ID(), player.target.GetComponent<Leader>().ID(), MessageEnum.PlayerStopShakeHands);
            player.GetFSM().ChangeState(player.States.WaitingState);
        }
       
    }

    public override void Exit(Player player)
    {
        player.PlayerAC.StopShakeHands();
        player.CleanWaittime();
    }

    public override bool OnMessage(Player player, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderStopShakeHands: player.GetFSM().ChangeState(player.States.WaitingState); break;
            case MessageEnum.LeaderReturned: MessageDispatcher.Instance().DisparcherMessage(Const.PLAYER_SHAKEHANDS_TIME, player.ID(), player.ID(), MessageEnum.LeaderReturned); break;
        }
        return true;
    }
    #endregion
}


