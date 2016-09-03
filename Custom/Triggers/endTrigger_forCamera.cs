using UnityEngine;
using System.Collections;

public class endTrigger_forCamera : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        GameController gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        MyCamera camera = GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<MyCamera>();
        //如果检测到主席进入了触发区域，就让游戏控制器主席发送消息。
        if (collider.gameObject.tag == Tags.Leader)
        {
            Leader leader = collider.gameObject.GetComponent<Leader>();
            camera.States.LeaderViewState.leader_View_Scene_Offset = new Vector3(0f, 2f, -1.7f);
            MessageDispatcher.Instance().DisparcherMessage(0.0f, gameController.ID(), camera.ID(), MessageEnum.StartSpeech);
        }

    }
}
