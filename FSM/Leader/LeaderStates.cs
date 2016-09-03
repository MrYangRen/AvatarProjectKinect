using UnityEngine;
using System.Collections;

public class LeaderStates : MonoBehaviour {

    public Leader_IdleState IdleState;
    public Leader_WalkingState WalkingState;
    public Leader_InteractiveState InteractiveState;
    public Leader_SaluteState SaluteState;
    public Leader_ShakeHandsState ShakeHandsState;
    public Leader_SpeechState SpeechState;
    public Leader_WaveHandState WaveHandState;
    public Leader_WaitingState WaitingState;
    public Leader_AfterSpeechState AfterSpeechState;
    public Leader_CloseToPlayerState CloseToPlayerState;
    public Leader_ReturnToPathState ReturnToPathState;

    void Awake()
    {
        IdleState = this.GetComponent<Leader_IdleState>();
        WalkingState = this.GetComponent<Leader_WalkingState>();
        InteractiveState = this.GetComponent<Leader_InteractiveState>();
        SaluteState = this.GetComponent<Leader_SaluteState>();
        ShakeHandsState = this.GetComponent<Leader_ShakeHandsState>();
        SpeechState = this.GetComponent<Leader_SpeechState>();
        WaveHandState = this.GetComponent<Leader_WaveHandState>();
        WaitingState = this.GetComponent<Leader_WaitingState>();
        AfterSpeechState = this.GetComponent<Leader_AfterSpeechState>();
        CloseToPlayerState=this.GetComponent<Leader_CloseToPlayerState>();
        ReturnToPathState = this.GetComponent<Leader_ReturnToPathState>();
    }
}
