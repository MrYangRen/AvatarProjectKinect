using UnityEngine;
using System.Collections;

public class AnimationTurnOnSpot : StateMachineBehaviour {

    private Leader_InteractiveState interactiveStateOfLeader;
    private float timer = 0.0f;

   

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        interactiveStateOfLeader = GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>().States.InteractiveState;
        timer = 0.0f;

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        timer += Time.deltaTime;
        if (timer >= 2f )
        {
            animator.SetFloat("Direction", 0.0f);
        }


    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        //Debug.LogWarning(Time.time + "exit turn on spot");
        //有三个地方会调用turnOnSpot，当状态转换出去的时候告诉调用者。首先判断谁调用了TurnOnSpot;
        interactiveStateOfLeader.SendMessage("SetRotateFinished", true);
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
}
