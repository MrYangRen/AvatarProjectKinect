using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
using System.Collections.Generic;
using System.Linq;

public class MyCamera : BaseGameEntity
{
    #region override
    //指向一个状态实例的指针
    StateMachine<MyCamera> m_pStateMachine;
    [HideInInspector]
    public CameraStates States;

    //Character list
    public Transform leader;
    public Transform player;

    private bool PlotSwitch = false;

    void Start()
    {
        // 设置ID
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).transform;
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        SetID((int)gameObject.GetInstanceID());


        //设置状态接口，并指向一个状态
        EntityManager.Instance().RegisterEntity(this);
        m_pStateMachine = new StateMachine<MyCamera>(this);
        States = this.gameObject.transform.FindChild("CameraStates").GetComponent<CameraStates>();
        m_pStateMachine = new StateMachine<MyCamera>(this);
        m_pStateMachine.SetCurrentState(States.FarSceneState);
        m_pStateMachine.SetGlobalStateState(States.GlobalState);
    }

    void Update()
    {
        //调用正在使用的状态		
        m_pStateMachine.SMUpdate();
    }

    public StateMachine<MyCamera> GetFSM()
    {
        //返回状态管理机
        return m_pStateMachine;
    }

    public override bool HandleMessage(Telegram msg)
    {
        if (!(m_pStateMachine.GlobalState().OnMessage(this, msg.Msg)))
        {
            Debug.Log("Message not handled");
            return false;
        }

        if (!(m_pStateMachine.CurrentState().OnMessage(this, msg.Msg)))
        {
            Debug.Log("Message not handled");
            return false;
        }
        return true;
    }
    #endregion


    #region 外部接口

    public void PlotSwitchOn() { PlotSwitch = true; }
    public void PlotSwitchOff(){ PlotSwitch = false; }

    public bool getPlotSwitch() { return PlotSwitch; }

    #endregion


    void ShowNPCInView()
    {
        Camera thisCamera = this.GetComponent<Camera>();

 
    }

}