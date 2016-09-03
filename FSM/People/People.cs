using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class People : HumanEntity {

    #region override
    //指向一个状态实例的指针
    StateMachine<People> m_pStateMachine;
    public LocomotionPeople PeopleAC;
    public PeopleStates States;

    void Start()
    {
        // 设置ID
        SetID(++EntityManager.lastEntityID);
        //设置状态接口，并指向一个状态
        PeopleAC = gameObject.GetComponent<LocomotionPeople>();
        EntityManager.Instance().RegisterEntity(this);
        States = this.gameObject.transform.FindChild("PeopleStates").GetComponent<PeopleStates>();
        m_pStateMachine = new StateMachine<People>(this);
        m_pStateMachine.SetCurrentState(States.IdleState);
        
    }

    void Update()
    {
        //调用正在使用的状态		
        m_pStateMachine.SMUpdate();
    }

    public StateMachine<People> GetFSM()
    {
        //返回状态管理机
        return m_pStateMachine;
    }
    public override bool HandleMessage(Telegram msg)
    {
        if (!(m_pStateMachine.HandleMessage(msg.Msg)))
        {
            Debug.Log("Message not handled");
            return false;
        }
        return true;
    }

    #endregion


}
