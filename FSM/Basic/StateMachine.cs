using UnityEngine;
using System.Collections;

public class StateMachine<entity_type>
{
    private entity_type m_pOwner;

    private State<entity_type> m_pCurrentState;
    private State<entity_type> m_pPreviousState;
    private State<entity_type> m_pGlobalState;

    public StateMachine(entity_type owner)
    {
        m_pOwner = owner;
        m_pCurrentState = null;
        m_pPreviousState = null;
        m_pGlobalState = null;
    }

    public void GlobalStateEnter()
    {
        //进入全局状态，并设置目标为m_pOwner
        m_pGlobalState.Owner=m_pOwner;
        m_pGlobalState.Enter(m_pOwner);
    }

    public void SetGlobalStateState(State<entity_type> GlobalState)
    {
        //设置全局状态，设置其目标为m_pOwner，并进入全局状态
        m_pGlobalState = GlobalState;
        m_pGlobalState.Owner = m_pOwner;
        m_pGlobalState.Enter(m_pOwner);
    }

    public void SetCurrentState(State<entity_type> CurrentState)
    {
        //设置当前状态，设置目标并进入
        m_pCurrentState = CurrentState;
        m_pCurrentState.Owner = m_pOwner;
        m_pCurrentState.Enter(m_pOwner);
    }


    public void SMUpdate()
    {
        //全局状态的运行
        if (m_pGlobalState != null)
            m_pGlobalState.Execute(m_pOwner);

        //一般当前状态的运行
        if (m_pCurrentState != null)
            m_pCurrentState.Execute(m_pOwner);
    }

    public void ChangeState(State<entity_type> pNewState)
    {
        if (pNewState == null)
        {
            Debug.LogError("该状态不存在");
        }

        //退出之前状态
        m_pCurrentState.Exit(m_pOwner);
        //保存之前状态
        m_pPreviousState = m_pCurrentState;
        //设置当前状态
        m_pCurrentState = pNewState;
        m_pCurrentState.Owner = m_pOwner;
        //进入当前状态
        m_pCurrentState.Enter(m_pOwner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(m_pPreviousState);
    }

    public State<entity_type> CurrentState()
    {

        return m_pCurrentState;
    }
    public State<entity_type> GlobalState()
    {

        return m_pGlobalState;
    }
    public State<entity_type> PreviousState()
    {
        return m_pPreviousState;
    }

    public bool HandleMessage(MessageEnum msg)
    {
    //如果当前状态不为空，就让当前状态处理消息，并返回处理成功

    	if (m_pCurrentState!=null && m_pCurrentState.OnMessage(m_pOwner, msg)) {
    		return true;
    		}
     
    //如果全局状态不为空，就让全局状态处理消息，并返回成功（当前状态的优先级更高）
    	if (m_pGlobalState!=null && m_pGlobalState.OnMessage (m_pOwner, msg)) {
    		return true;
    		}

    	return false;
    }
}