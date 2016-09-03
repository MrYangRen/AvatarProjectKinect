using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;

public class People_IdleState : State<People>
{

    #region override
    public override void Enter(People people)
    {
        people.CleanWaittime();
        people.PeopleAC.StopWalking();
        }

    public override void Execute(People people)
    {
        people.PeopleAC.StopWalking();
    }

    public override void Exit(People people)
    {
        people.CleanWaittime();
    }

    public override bool OnMessage(People people, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StartWalking: people.GetFSM().ChangeState(people.States.WalkingState); break;
            case MessageEnum.LeaderReturned: people.GetFSM().ChangeState(people.States.WalkingState); break;
            default: return false;
        }
        return true;
    }
    #endregion
}
