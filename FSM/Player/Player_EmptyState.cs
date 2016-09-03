using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;



public class Player_EmptyState : State<Player>
{


    #region override
    public override void Enter(Player player)
    {

    }

    public override void Execute(Player player)
    {
        if (GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer())
        {
            player.GetFSM().ChangeState(player.States.IdleState);
        }


    }

    public override void Exit(Player player)
    {

    }

    public override bool OnMessage(Player player, MessageEnum msg)
    {
        return true;
    }

    #endregion



}
