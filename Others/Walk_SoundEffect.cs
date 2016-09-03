using UnityEngine;
using System.Collections;

public class Walk_SoundEffect : MonoBehaviour {

    private AudioSource audiosource_walk;
    private Leader leader;
    private State<Leader> lastLeaderState;
	// Use this for initialization
	void Start () {
	    audiosource_walk = this.transform.GetComponent<AudioSource>();
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>();
        lastLeaderState = leader.GetFSM().CurrentState();
	}
	
	// Update is called once per frame
	void Update () {

        if (leader.GetFSM().CurrentState() == leader.States.WalkingState && lastLeaderState != leader.States.WalkingState)
        {
            audiosource_walk.Play();
        }
        else if (lastLeaderState == leader.States.WalkingState && leader.GetFSM().CurrentState() != leader.States.WalkingState)
        {
            audiosource_walk.Stop();
        }
        lastLeaderState = leader.GetFSM().CurrentState();
	}
}
