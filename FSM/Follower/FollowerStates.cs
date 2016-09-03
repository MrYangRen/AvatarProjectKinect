using UnityEngine;
using System.Collections;

public class FollowerStates : MonoBehaviour {


    public Follower_IdleState IdleState;
    public Follower_WalkingState WalkingState;
    public Follower_HandClapingState HandClapingState;
    public Follower_ListeningState ListeningState;
    public Follower_WarningState WarningState;


    void Awake()
    {
        IdleState = this.GetComponent<Follower_IdleState>();
        WalkingState = this.GetComponent<Follower_WalkingState>();
        HandClapingState = this.GetComponent<Follower_HandClapingState>();
        ListeningState = this.GetComponent<Follower_ListeningState>();
        WarningState = this.GetComponent<Follower_WarningState>();
    }

}
