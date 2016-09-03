using UnityEngine;
using System.Collections;

//目前只用单例模式
public class People_HandClapingState : State<People> {


    #region override
    public override void Enter(People people)
    {
        people.CleanWaittime();

    }

    public override void Execute(People people)
    {
        people.PlusWaittime();
        if (people.GetWaittime() > Const.PEOPLE_CLAPHANDS_TIME)
        {
            people.GetFSM().ChangeState(people.States.IdleState);
        }

    }

    public override void Exit(People people)
    {
        people.CleanWaittime();
    }


    public override bool OnMessage(People people, MessageEnum msg)
    {
        //switch (msg)
        //{
        //    default: return false;
        //}
        return true;
    }
    #endregion
}
