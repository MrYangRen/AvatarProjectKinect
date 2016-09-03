using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;



public class Leader_CloseToPlayerState : State<Leader>
{

    #region override
    public override void Enter(Leader leader)
    {
        leader.LeaderAC.Stop();
        if (GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer())
        {
            MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), leader.target.GetComponent<Player>().ID(), MessageEnum.LeaderCloseToPlayer);
        }
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<MyCamera>().ID(), MessageEnum.LeaderCloseToPlayer);
        leader.CleanWaittime();
    }

    public override void Execute(Leader leader)
    {
        //leader.LeaderAC.Stop();
        leader.PlusWaittime();
        if (GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer())
        {
            if (leader.GetWaittime() > 0.05)
            {
                approchPlayer(leader);
            }
            else
            {
                leader.LeaderAC.Stop();
            }
        }
        else
        {
            leader.GetFSM().ChangeState(leader.States.WalkingState);
        }
    }

    public override void Exit(Leader leader)
    {
        leader.GetComponent<Vehicle>().enabled = false;
        leader.gameObject.GetComponent<SteerForPursuit>().enabled = false;
        leader.CleanWaittime();
    }

    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        //switch (msg)
        //{
        //    default: return false;
        //}
        return true;
    }
    #endregion

    #region 内部服务函数

    private void approchPlayer(Leader leader)
    {
        leader.GetComponent<Vehicle>().enabled = true;
        leader.GetComponent<SteerForPursuit>().Quarry = leader.target.GetComponent<AutonomousVehicle>();
        leader.GetComponent<SteerForPursuit>().enabled = true;
        leader.LeaderAC.Move();
        if (Vector3.Distance(leader.transform.position, leader.target.transform.position) < Const.INTERACTIVE_DISTANCE)
        {
            leader.GetComponent<SteerForPursuit>().enabled = false;
            leader.LeaderAC.Stop();
            leader.GetFSM().ChangeState(leader.States.WaitingState);
        }
    }

    IEnumerator waitForSeconds(float seconds)
    {
        float timer = 0.0f;
        timer += Time.deltaTime;
        while (timer < seconds)
        {
            yield return 0;
        }
    }
    #endregion

}
