using UnityEngine;
using System.Collections;

public class Player_HandClapingState : State<Player> {

    #region override
    public override void Enter(Player player)
    {
        player.CleanWaittime();
        player.PlayerAC.ClapHands();

    }

    public override void Execute(Player player)
    {
        //if (!GUItest.playerClapHands)
        //{
        //    player.GetFSM().ChangeState(player.States.IdleState);
        //}
        player.PlusWaittime();
        if(player.GetWaittime()>Const.PLAYER_CLAPHANDS_TIME)
            player.GetFSM().ChangeState(player.States.IdleState);

    }

    public override void Exit(Player player)
    {
        player.CleanWaittime();
        player.PlayerAC.StopClapHands();
    }


    public override bool OnMessage(Player player, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StartWalking: player.GetFSM().ChangeState(player.States.WalkingState); break;
            default: return false;
        }
        return true;
    }
    #endregion
}
