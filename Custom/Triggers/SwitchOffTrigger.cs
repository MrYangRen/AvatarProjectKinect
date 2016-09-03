using UnityEngine;
using System.Collections;

public class SwitchOffTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == Tags.Leader)
        {
            GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>().States.WalkingState.TalkSwitchOff();
            GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().InteractiveSwitchOff();

            //打开相机的固定剧情模式开关
            GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<MyCamera>().PlotSwitchOn();
        }
    }
}
