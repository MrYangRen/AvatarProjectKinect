using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
using System.Collections.Generic;
using System.Linq;

//目前仍需解决的问题：Follower的停下。还有就是那个什么

public class Leader : HumanEntity
{
    #region override
    //指向一个状态实例的指针
    StateMachine<Leader> m_pStateMachine;
    public LocomotionLeader LeaderAC;
    public AudioClip[] arraryOfVoices;
    public Dictionary<string, AudioClip> LeaderVoices = new Dictionary<string, AudioClip>();
    public AudioSource voice;
    public LeaderStates States;
    void Start()
    {
        // 设置ID
        SetID(++EntityManager.lastEntityID);
        //设置状态接口，并指向一个状态
        LeaderAC = this.gameObject.GetComponent<LocomotionLeader>();
        for (int i = 0; i < arraryOfVoices.Length; i++)
        {
            LeaderVoices.Add(arraryOfVoices[i].name, arraryOfVoices[i]);
        }
        voice = this.gameObject.GetComponent<AudioSource>();
        EntityManager.Instance().RegisterEntity(this);
        States = this.gameObject.transform.FindChild("LeaderStates").GetComponent<LeaderStates>();
        m_pStateMachine = new StateMachine<Leader>(this);
        m_pStateMachine.SetCurrentState(States.IdleState);
    }

    void Update()
    {
        //调用正在使用的状态		
        m_pStateMachine.SMUpdate();
    }

    public StateMachine<Leader> GetFSM()
    {
        //返回状态管理机
        return m_pStateMachine;
    }

    public override bool HandleMessage(Telegram msg)
    {
        if (!(m_pStateMachine.HandleMessage(msg.Msg)))
        {
            //Debug.Log("Message not handled");
            return false;
        }
        return true;
    }

    #endregion
}