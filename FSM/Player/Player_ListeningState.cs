using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Player_ListeningState : State<Player> {

    #region 成员变量
    private bool ClapHandsSwitch = false;
    private bool rotateFinished = false;
    #endregion


    #region override
    public override void Enter(Player player)
    {
        player.CleanWaittime();
        player.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        player.PlayerAC.TurnOnSpot(player.target.transform.position - player.gameObject.transform.position);

    }

    public override void Execute(Player player)
    {
        if (ClapHandsSwitch)
        {
            TipAnimation.SetPlayerCanClapHands(true);
            if (GUItest.playerClapHands)
            {
                player.GetFSM().ChangeState(player.States.HandClapingState);

            }
        }
        else if (player.GetWaittime() < Const.PLAYER_CLAPHANDS_WAIT_TIME)
        {
            TipAnimation.SetPlayerCanClapHands(false);
        }
        else
        {
            player.GetFSM().ChangeState(player.States.IdleState);

        }


    }

    public override void Exit(Player player)
    {
        TipAnimation.SetPlayerCanClapHands(false);
        player.CleanWaittime();
        ClapHandsSwitch = false;
    }


    public override bool OnMessage(Player player, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderStopSpeech: ClapHandsSwitch = true; break;
            default: return false;
        }
        return true;

    }
    #endregion



}
