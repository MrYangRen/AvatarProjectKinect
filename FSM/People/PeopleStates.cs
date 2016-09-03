using UnityEngine;
using System.Collections;

public class PeopleStates : MonoBehaviour {

    public People_IdleState IdleState;
    public People_WalkingState WalkingState;
    public People_HandClapingState HandClapingState;
    public People_ListeningState ListeningState;


    void Awake()
    {
        IdleState = this.GetComponent<People_IdleState>();
        WalkingState = this.GetComponent<People_WalkingState>();
        HandClapingState = this.GetComponent<People_HandClapingState>();
        ListeningState = this.GetComponent<People_ListeningState>();
 
    }
}
