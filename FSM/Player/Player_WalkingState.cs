using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class Player_WalkingState : State<Player>
{

    #region 成员变量
    private Vector3 ForwardWhenExit;
    #endregion

    #region override
    public override void Enter(Player player)
    {
        player.CleanWaittime();
        player.gameObject.GetComponent<SteerForLeadFollowing>().enabled = true;
        player.gameObject.GetComponent<SteerForLeadFollowing>().Quarry = player.target.GetComponent<AutonomousVehicle>();
        player.PlayerAC.Stop();
        
    }

    public override void Execute(Player player)
    {
        //如何让玩家在行走的时候保证一直面朝主席
        //如果检测到玩家挥手，就让玩家进入交互状态，并设置交互类型，发送消息给Follower让Follower
        //进入warning状态
        player.gameObject.transform.LookAt(player.target.gameObject.transform.position);
        if (player.GetInteractiveSwitch())
        {
            if (GUItest.playerWaveHands)
            {
                player.States.WaveHandState.SetEntrance(Entrance.FromWalking);
                player.GetFSM().ChangeState(player.States.WaveHandState);
            }
            if (GUItest.playerSalute)
            {
                player.States.SaluteState.SetEntrance(Entrance.FromWalking);
                player.GetFSM().ChangeState(player.States.SaluteState);
            }
            //if (!GUItest.playerWalk)
            //{
            //    player.GetFSM().ChangeState(player.States.IdleState);
            //}
        }
        player.PlayerAC.FollowLeader();
    }

    public override void Exit(Player player)
    {
        player.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        player.PlayerAC.Stop();
        player.CleanWaittime();
        ForwardWhenExit = player.gameObject.transform.forward;
    }

    public override bool OnMessage(Player player, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StopWalking: player.GetFSM().ChangeState(player.States.IdleState); break;

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
