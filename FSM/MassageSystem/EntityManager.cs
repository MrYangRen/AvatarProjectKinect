using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//电话本，存储发送者和接收者的信息
public class EntityManager
{
    private static EntityManager _instance;//单例化

    public Dictionary<int, BaseGameEntity> EntityMap = new Dictionary<int, BaseGameEntity>();//声明字典保存

    public static int lastEntityID=0;
    
    public static EntityManager Instance()
    {
        if (_instance == null)
            _instance = new EntityManager();
        return _instance;
    }
    //通过ID来找到BaseGameEntity
    public BaseGameEntity GetEntityFromID(int id)
    {
        BaseGameEntity info = null;
        EntityMap.TryGetValue(id, out info);
        return info;
    }

    //从字典中移除entity
    public void RemoveEntity(BaseGameEntity pEntity)
    {
        EntityMap.Remove(pEntity.GetID());
    }

    //在字典中注册entity的信息
    public void RegisterEntity(BaseGameEntity NewEntity)
    {
        EntityMap.Add(NewEntity.GetID(), NewEntity);
    }
}
