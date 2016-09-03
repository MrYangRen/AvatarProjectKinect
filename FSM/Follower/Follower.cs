using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
using System.Collections.Generic;
using System.Linq;


public class Follower : HumanEntity
{
    #region override
    //指向一个状态实例的指针
    StateMachine<Follower> m_pStateMachine;
    
   //用FollowerAnimatorController 
    public LocomotionFollower FollowerAC;
    public AudioClip[] arraryOfVoices;

    public Dictionary<string, AudioClip> FollowerVoices = new Dictionary<string, AudioClip>();
    public AudioSource voice;
    public FollowerStates States;

    void Start()
    {
        // 设置ID
        SetID(++EntityManager.lastEntityID);
        target = GameObject.FindGameObjectWithTag(Tags.Leader);
        FollowerAC = this.gameObject.GetComponent<LocomotionFollower>();
        for (int i = 0; i < arraryOfVoices.Length; i++)
        {
            FollowerVoices.Add(arraryOfVoices[i].name, arraryOfVoices[i]);
        }
        voice = this.gameObject.GetComponent<AudioSource>();

        EntityManager.Instance().RegisterEntity(this);
        States = this.gameObject.transform.FindChild("FollowerStates").GetComponent<FollowerStates>();
        m_pStateMachine = new StateMachine<Follower>(this);
        m_pStateMachine.SetCurrentState(States.IdleState);
       
    }

    void Update()
    {
        //调用正在使用的状态		
        m_pStateMachine.SMUpdate();
    }

    public StateMachine<Follower> GetFSM()
    {
        //返回状态管理机
        return m_pStateMachine;
    }

    public override bool HandleMessage(Telegram msg)
    {
        if (!(m_pStateMachine.CurrentState().OnMessage(this, msg.Msg)))
        {
            Debug.Log("Message not handled");
            return false;
        }
        return true;
    }

    #endregion
}