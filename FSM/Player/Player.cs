using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;
using System.Collections.Generic;
using System.Linq;
//Player无论在哪个位置，都要一直保持朝向主席
public class Player : HumanEntity {
    #region override
    //指向一个状态实例的指针
    StateMachine<Player> m_pStateMachine;
    public  LocomotionPlayer PlayerAC;
    public AudioClip[] arraryOfVoices;
    public Dictionary<string, AudioClip> PlayerVoices = new Dictionary<string, AudioClip>();
    public AudioSource voice;
    private bool interactiveSwitch = true;
    public PlayerStates States;

    void Start()
    {
        PlayerAC = gameObject.GetComponent<LocomotionPlayer>();
        // 设置ID
        SetID(++EntityManager.lastEntityID);

        for (int i = 0; i < arraryOfVoices.Length; i++)
        {
            PlayerVoices.Add(arraryOfVoices[i].name, arraryOfVoices[i]);
        }
        voice = this.gameObject.GetComponent<AudioSource>();

        EntityManager.Instance().RegisterEntity(this);
        target = GameObject.FindGameObjectWithTag(Tags.Leader);
        States = this.gameObject.transform.FindChild("PlayerStates").GetComponent<PlayerStates>();
        m_pStateMachine = new StateMachine<Player>(this);
        m_pStateMachine.SetCurrentState(States.EmptyState);
       
    }

    void Update()
    {
        //调用正在使用的状态		
        m_pStateMachine.SMUpdate();
        if (!GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer())
        {
            m_pStateMachine.ChangeState(States.EmptyState);
        }
    }

    public StateMachine<Player> GetFSM()
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
    public void InteractiveSwitchOn() { interactiveSwitch = true; }
    public void InteractiveSwitchOff() { interactiveSwitch = false; }
    public bool GetInteractiveSwitch() { return interactiveSwitch; }
    #endregion


}