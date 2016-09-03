using UnityEngine;
using System.Collections;

public class NPCClapHands : MonoBehaviour {

    public LocomotionPeople PeopleAC;
    private float timeclock;

    //这里跟随follower鼓掌，因为这些people没有状态机，收不到主席的消息，而只有follower不在people中，却会鼓掌
    public Follower follower = null;
	// Use this for initialization
	void Start () {
        PeopleAC = this.gameObject.GetComponent<LocomotionPeople>();
        follower = GameObject.FindGameObjectWithTag(Tags.Follower).GetComponent<Follower>();
       // follower.CleanWaittime();
        timeclock = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (follower.GetFSM().CurrentState() == follower.States.HandClapingState)
       {
          // follower.PlusWaittime();
           timeclock += Time.deltaTime;
          // Debug.Log("follower is claping hands");
           PeopleAC.ClapHands();
           ClapHandsEffect.Instance().StartClapHandsEffet();
           
       }
        if (follower.GetWaittime() >= 3.2)
        {
            PeopleAC.StopClapHands();
            ClapHandsEffect.Instance().StopClapHandsEffet();
        }

        //follower.CleanWaittime();
        timeclock = 0;

	}
}
