using UnityEngine;
using System.Collections;
public class BaseGameEntity : MonoBehaviour
{

    private int m_ID;

    private static ArrayList m_idArray = new ArrayList();

    public int ID()
    {
        return m_ID;
    }

    protected void SetID(int val)
    {

        //这个函数用来确认ID是否正确设置
        if (m_idArray.Contains(val))
        {
            Debug.LogError("id 错误 ");
            return;
        }
        m_idArray.Add(val);
        m_ID = val;
    }

    public int GetID()
    {
        return m_ID;
    }

    //应该分配到每一个状态当中去处理
    public virtual bool HandleMessage(Telegram msg)
    {
        return true;
    }

}