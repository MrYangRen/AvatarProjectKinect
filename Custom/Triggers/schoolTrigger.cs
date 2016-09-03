using UnityEngine;
using System.Collections;

public class schoolTrigger : MonoBehaviour {


    //触发器的消息一律委托给游戏控制器来发送。不再为触发器再单独建立Entity
    void OnTriggerEnter(Collider collider)
    {
        GameController gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        //如果检测到主席进入了触发区域，就让游戏控制器主席发送消息。
        if (collider.gameObject.tag == Tags.Leader)
        {
            Leader leader = collider.gameObject.GetComponent<Leader>();
            //先让主席停下来
            MessageDispatcher.Instance().DisparcherMessage(0.0f, gameController.ID(), leader.ID(), MessageEnum.StopWalking);

            TipAnimation.SetShowFrontSchoolCaption(true);
            //然后再让主席发表演讲
            leader.States.SpeechState.setSpeechContent(AudioClipHash.Leader_SpecialScene02);
           
            //可调参数：延时发送消息的时间！
            MessageDispatcher.Instance().DisparcherMessage(15.0f, gameController.ID(), leader.ID(), MessageEnum.StartSpeech);
            BGM.Instance().FadeOutBGM();
        }

    }
}
