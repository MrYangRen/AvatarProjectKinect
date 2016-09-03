using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//改动，因为队列的排序不方便，所以直接将队列
public class MessageDispatcher
{
    private static MessageDispatcher _instance;
    private List<Telegram> PriorityQ = new List<Telegram>();//需要延迟发送的消息存在队列中

    public static MessageDispatcher Instance()
    {
        if (_instance == null)
            _instance = new MessageDispatcher();

        return _instance;
    }

    private void DisCharge(BaseGameEntity pReceiver, Telegram msg)
    {
        if (!pReceiver.HandleMessage(msg))
        {
            //Debug.Log("Message not handled");
        }
        //else 
        //{
        //    Debug.Log("Message handled");
        //}
    }

    //点对面发送消息
    public void DisparcherMessage(double delay, int sender, MessageEnum msg)
    {
        BaseGameEntity pSender = EntityManager.Instance().GetEntityFromID(sender);
        Telegram telegram = new Telegram();
        //为什么这里设置信息不直接用delay
        telegram.SetTelegram(0, sender, msg);

        //如果没有延迟的话，就调用处理函数Discharge，这个函数会调用handlemessage函数
        if (delay <= 0.0f)
        {
            //遍历所有的basegameobject将消息发送出去，有处理这个方法的状态就会处理，没有的不做处理
            //遍历可能出错，检查的时候注意下！！！
            foreach (KeyValuePair<int, BaseGameEntity> pvr in EntityManager.Instance().EntityMap)
            {
                DisCharge(pvr.Value, telegram);
            }
        }

        else
        {
            double CurrentTime = Time.time;
            telegram.DispatchTime = CurrentTime + delay;

            PriorityQ.Add(telegram);//把信息加入到链表中

        }
    }

    //点对点发送消息
    public void DisparcherMessage(double delay, int sender, int receiver, MessageEnum msg)
    {
        BaseGameEntity pSender = EntityManager.Instance().GetEntityFromID(sender);
        BaseGameEntity pReceiver = EntityManager.Instance().GetEntityFromID(receiver);
        if (pReceiver == null)
        {
            return;
        }

        Telegram telegram = new Telegram();
        telegram.SetTelegram(0, sender, receiver, msg);

        //如果没有延迟的话，就调用处理函数Discharge，这个函数会调用handlemessage函数
        if (delay <= 0.0f)
        {
            DisCharge(pReceiver, telegram);
        }

        //需要延迟发送的话就计算时间，计算要发送的时间，在delayDispatcher中根据时间判断并发送
        else
        {
            double CurrentTime = Time.time;
            telegram.DispatchTime = CurrentTime + delay;

            PriorityQ.Add(telegram);//把信息加入到链表中

        }
    }

    //延迟发送消息
    public void DispatcherDelayedMesages()
    {
        double CurrentTime = Time.time;

        PriorityQ = (from telegram in PriorityQ orderby telegram.DispatchTime ascending select telegram).ToList();

        while (PriorityQ.Count != 0 && (PriorityQ[0].DispatchTime < CurrentTime) &&
            (PriorityQ[0].DispatchTime > 0))
        {
            Telegram telegram = PriorityQ[0];//取出头元素
            PriorityQ.RemoveAt(0);          //并删除

            BaseGameEntity pReceiver = EntityManager.Instance().GetEntityFromID(telegram.Receiver);
            DisCharge(pReceiver, telegram);
        }
    }

}