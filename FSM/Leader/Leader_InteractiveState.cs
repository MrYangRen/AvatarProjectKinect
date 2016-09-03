using UnityEngine;
using System.Collections;
using UnitySteer.Behaviors;


//交互类型
public enum InteractiveType
{
    none,
    WaveHands,
    Salute
}
//入口（从行走状态进入还是从静止状态进入）
public enum Entrance
{
    FromWalking,
    FromIdling,
}

public class Leader_InteractiveState : State<Leader>
{

    #region 成员变量
    Entrance entrance;
    bool needApproch;
    InteractiveType interactive;
    Vector3 lastPosition;
    Vector3 lastForward;
    bool rotateFinished=false;
    Vector3 targetDirection;

    #endregion

    #region override
    public override void Enter(Leader leader)
    {
        rotateFinished = false;
        leader.CleanWaittime();
        leader.LeaderAC.Stop();
        #region 保存现场和发送消息
        //主席进入交互状态，给所有人发送消息告诉他们主席要去交互了，people和follower都要停下来等待主席
        MessageDispatcher.Instance().DisparcherMessage(0.0f, leader.ID(), MessageEnum.LeaderGotoInteractive);
        //记录从哪个状态转换而来，便于返回
        switch (entrance)
        {
            case Entrance.FromIdling: lastPosition = leader.States.IdleState.GetPositionWhenExit(); lastForward = leader.States.IdleState.GetForwardWhenExit(); break;
            case Entrance.FromWalking: lastPosition = leader.States.WalkingState.GetPositionWhenExit(); lastForward = leader.States.WalkingState.GetForwardWhenExit(); break;
        }
        #endregion
        
        #region 根据概率设置是否要走向玩家
        //每一次有新交互的时候会重新根据概率设置是否需要接近玩家。
        //如果是交互完成的情况则会保留记录这个变量，然后可以走回去。

        float rand = Random.Range(0,10);
        //60%概率接近玩家
        if (rand < Const.LEADER_POSSIBILITY_OF_CLOSE_TO_PLAYER*10)
        {
            needApproch = true;
        }
        else
        {
            needApproch = false;
        }

        #endregion 
        #region 根据不同情况设置不同朝向
        if (interactive == InteractiveType.none)
        {
            targetDirection = lastForward;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer())
            {
                targetDirection = leader.target.transform.position - leader.gameObject.transform.position;
            }
        }
        #endregion
        StartCoroutine(waitForSeconds(0.5f));
        leader.LeaderAC.TurnOnSpot(targetDirection);
    }
   
    public override void Execute(Leader leader)
    {
        if (!GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer())
        {
            leader.GetFSM().ChangeState(leader.States.WalkingState);
          }
        //如果转身完毕，就回到入口或者走进玩家
        if (rotateFinished)
        {
            
            //leader.LeaderAC.Stop();
            if (interactive == InteractiveType.none)
            {

                MessageDispatcher.Instance().DisparcherMessage(0.0, leader.ID(), MessageEnum.LeaderReturned);
                RevertToEntrance(leader);
            }
            //如果不需要接近玩家，就原地转身并转移到对应状态
            else if (!needApproch)
            {
                switch (interactive)
                {
                    case InteractiveType.Salute: leader.GetFSM().ChangeState(leader.States.SaluteState); break;
                    case InteractiveType.WaveHands: leader.GetFSM().ChangeState(leader.States.WaveHandState); break;
                }
            }
            //如果需要接近玩家，就接近玩家
            else
            {
                leader.GetFSM().ChangeState(leader.States.CloseToPlayerState);
            }
        }
    }

    public override void Exit(Leader leader)
    {
        //leader.LeaderAC.Stop();
        leader.CleanWaittime();
    }

    public override bool OnMessage(Leader leader, MessageEnum msg)
    {
        //switch (msg)
        //{
        //    default: return false;
        //}
        return true;
    }
    #endregion

    #region 外部接口
    //给入口状态的接口
    public void SetInteractiveType(InteractiveType interactiveType)
    {
        interactive = interactiveType;
    }
    public InteractiveType GetInteractiveType() { return interactive; }
    public void SetEntrance(Entrance entrance)
    {
        this.entrance = entrance;
    }
    public Entrance GetEntrance() { return entrance; }

    public Vector3 GetLastPosition() { return lastPosition; }
    public bool GetNeedApproch() { return needApproch; }
    public void SetRotateFinished(bool flag) { rotateFinished = flag; }

    #endregion

    # region 内部服务函数
    private void RevertToEntrance(Leader leader)
    {
        if (entrance == Entrance.FromIdling)
        {
            leader.GetFSM().ChangeState(leader.States.IdleState);
        }
        else if (entrance == Entrance.FromWalking)
        {
            leader.GetFSM().ChangeState(leader.States.WalkingState);
        }
    }


    IEnumerator waitForSeconds(float seconds)
    {
        float timer = 0.0f;
        timer += Time.deltaTime;
        while(timer < seconds)
        {
            yield return 0;
        }
        
    }
    #endregion

}
