using UnityEngine;
using System.Collections;



public abstract class State<entity_type> :MonoBehaviour
{

    public entity_type Owner;
    //进入状态  
    public virtual void Enter(entity_type Owner) { }
    //状态正常执行
    public virtual void Execute(entity_type Owner) { }

    //退出状态
    public virtual void Exit(entity_type Owner) { }

    //接受消息
    public virtual bool OnMessage(entity_type Owner, MessageEnum msg) { return true; }

    public virtual bool OnMessage(entity_type Owner) { return true; }


}