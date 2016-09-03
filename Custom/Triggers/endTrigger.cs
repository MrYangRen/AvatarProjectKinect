using UnityEngine;
using System.Collections;

public class endTrigger : MonoBehaviour
{

    public int nextScene = 2;
    private float timer = 0.0f;
    public UIWidget blackBG;
    public AudioSource audiosource;
    private Leader leader;
    private Follower follower;

    void Start()
    {
        // animator = this.gameObject.GetComponent<Animator>();
        //locomotion = new Locomotion(animator);
        audiosource = this.gameObject.GetComponent<AudioSource>();
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>();
        follower = GameObject.FindGameObjectWithTag(Tags.Follower).GetComponent<Follower>();

    }
    void OnTriggerEnter(Collider collider)
    {
        GameController gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        if (collider.gameObject.tag == Tags.Leader)
        {
            // Leader leader = collider.gameObject.GetComponent<Leader>();
            //先让主席停下来
            MessageDispatcher.Instance().DisparcherMessage(0.0f, gameController.ID(), leader.ID(), MessageEnum.StopWalking);
            audiosource.Play();
            leader.LeaderAC.MaskWaveHand();
            //follower.GetFSM().ChangeState(follower.States.HandClapingState);
            StartCoroutine(FadeToNextScene());
        }

    }


    IEnumerator FadeToNextScene()
    {
        while (timer < 1.0f) { timer += Time.deltaTime; yield return 0; }
        blackBG.GetComponent<UITweener>().PlayForward();
        timer = 0.0f;
        while (timer < 8.0)
        { timer += Time.deltaTime; yield return 0; }

        //audiosource.Stop();
        //leader.LeaderAC.StopWaveHand();
        Application.LoadLevel(nextScene);
    }



}