using UnityEngine;
using System.Collections;

public class SwitchOnTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        Leader leader = GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>();
        if (collider.gameObject.tag == Tags.Leader)
        {
            leader.States.WalkingState.TalkSwitchOn();
            GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().InteractiveSwitchOn();

            GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<MyCamera>().PlotSwitchOff();
        }
    }
}
