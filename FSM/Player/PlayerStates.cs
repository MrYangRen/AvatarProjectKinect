using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {

    public Player_EmptyState EmptyState;
    public Player_IdleState IdleState;
    public Player_CloseToLeaderState CloseToLeaderState;
    public Player_ListeningState ListeningState;
    public Player_HandClapingState HandClapingState;
    public Player_WalkingState WalkingState;
    public Player_SaluteState SaluteState;
    public Player_WaveHandState WaveHandState;
    public Player_ShakeHandsState ShakeHandsState;
    public Player_WaitingState WaitingState;


    void Awake()
    {
        IdleState = this.GetComponent<Player_IdleState>();
        EmptyState = this.GetComponent<Player_EmptyState>();
        CloseToLeaderState = this.GetComponent<Player_CloseToLeaderState>();
        ListeningState = this.GetComponent<Player_ListeningState>();
        HandClapingState = this.GetComponent<Player_HandClapingState>();
        WalkingState = this.GetComponent<Player_WalkingState>();
        SaluteState = this.GetComponent<Player_SaluteState>();
        WaveHandState = this.GetComponent<Player_WaveHandState>();
        ShakeHandsState = this.GetComponent<Player_ShakeHandsState>();
        WaitingState = this.GetComponent<Player_WaitingState>();


    }

}
