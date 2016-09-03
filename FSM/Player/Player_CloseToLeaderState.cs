using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;



public class Player_CloseToLeaderState : State<Player>
{



    #region override
    public override void Enter(Player player)
    {
        player.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        player.CleanWaittime();
    }

    public override void Execute(Player player)
    {
        //if (GUItest.playerWaveHands)
        //{
        //    player.GetFSM().ChangeState(player.States.WaveHandState);
        //}
        //if (GUItest.playerSalute)
        //{
        //    player.GetFSM().ChangeState(player.States.SaluteState);
        //}
        approchLeader(player);
    }

    public override void Exit(Player player)
    {
        player.gameObject.GetComponent<SteerForPursuit>().enabled = false;
        player.CleanWaittime();
    }

    public override bool OnMessage(Player player, MessageEnum msg)
    {
        //switch (msg)
        //{
        //    default: return false;
        //}
        return true;
    }
    #endregion

    #region 内部服务函数

    private void approchLeader(Player player)
    {
        player.GetComponent<SteerForPursuit>().Quarry = player.target.GetComponent<AutonomousVehicle>();
        player.GetComponent<SteerForPursuit>().enabled = true;
        player.PlayerAC.Move();
        if (Vector3.Distance(player.transform.position, player.target.transform.position) < Const.INTERACTIVE_DISTANCE)
        {
            player.GetComponent<SteerForPursuit>().enabled = false;
            player.PlayerAC.Stop();
            player.GetFSM().ChangeState(player.States.WaitingState);
        }
    }

    #endregion

}
