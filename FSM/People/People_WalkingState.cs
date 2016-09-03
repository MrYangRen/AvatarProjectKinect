using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
public class People_WalkingState : State<People>
{
    #region override
    public override void Enter(People people)
    {
        people.CleanWaittime();
        people.PeopleAC.Walking();
    }

    public override void Execute(People people)
    {
        people.PeopleAC.Walking();
    }

    public override void Exit(People people)
    {
        people.PeopleAC.StopWalking();
        people.CleanWaittime();
    }

    public override bool OnMessage(People people, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StopWalking: people.GetFSM().ChangeState(people.States.IdleState); break;
            case MessageEnum.LeaderGotoInteractive: people.GetFSM().ChangeState(people.States.IdleState); break;
            default: return false;
        }
        return true;
    }
    #endregion
}