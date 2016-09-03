using UnityEngine;
using System.Collections;

//存储着每条消息的信息，发送者、接收者、信息内容、发送时间
public class Telegram
{
    public int Sender = -1;// 发送者的ID
    public int Receiver = -1; //接收者的ID
    public MessageEnum Msg;
    public double DispatchTime = -1f;


    //广播消息
    public void SetTelegram(double time, int sender, MessageEnum msg)
    {
        DispatchTime = time;
        Sender = sender;
        Msg = msg;
    }

    public void SetTelegram(double time, int sender, int receiver, MessageEnum msg)
    {
        DispatchTime = time;
        Sender = sender;
        Receiver = receiver;
        Msg = msg;
    }
}

public enum MessageEnum
{
//相机发出
   StartWalking,
   StopWalking,
   StartSpeech,
//玩家发出
   PlayerWantsInteractive,
   PlayerSalute,
   PlayerWaveHands,
   PlayerShakeHands,
   PlayerStopShakeHands,
//主席发出
   //主席在退出每一个具体交互类的时候发送给玩家。玩家接受到之后马上进入等待状态
   LeaderStopShakeHands,
   //主席在进入每一个具体交互状态的时候发给所有人，所有人要做出反应（玩家对这个消息不做处理）
   LeaderGotoInteractive,
   LeaderReturned,
   LeaderCloseToPlayer,
   LeaderStopSpeech,
   LeaderStopInteractive
}

