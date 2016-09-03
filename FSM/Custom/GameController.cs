using UnityEngine;
using System.Collections;

//负责监测玩家动作，连接Kinect.以及发送延迟消息
public class GameController : HumanEntity
{
    #region override
    //指向一个状态实例的指针
    StateMachine<GameController> m_pStateMachine;

    //将来可能会交互到的对象。现在只可能给Leader发送消息
    public Leader leader;
    public Player player;
    void Start()
    {
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        // 设置ID
        SetID(++EntityManager.lastEntityID);
        //设置状态接口，并指向一个状态
        m_pStateMachine = new StateMachine<GameController>(this);
        m_pStateMachine.SetCurrentState(this.GetComponent<GameControllerState>());
        EntityManager.Instance().RegisterEntity(this);

    }

    void Update()
    {
        //调用正在使用的状态		
        MessageDispatcher.Instance().DispatcherDelayedMesages();
        m_pStateMachine.SMUpdate();
    }

    public StateMachine<GameController> GetFSM()
    {
        //返回状态管理机
        return m_pStateMachine;
    }
    #endregion


}