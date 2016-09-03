using UnityEngine;
using System.Collections;

public class HumanTrigger : MonoBehaviour {

   // private Animator animator;
   // private Locomotion locomotion = null;

    private AudioSource audiosource;
    private LocomotionPeople PeopleAC;
    private float clock;

    //这个的前提是要所有类型的people都有类似的mecanim
    void Start()
    {
        // animator = this.gameObject.GetComponent<Animator>();
        //locomotion = new Locomotion(animator);
         audiosource = this.gameObject.GetComponent<AudioSource>();
         PeopleAC = this.gameObject.GetComponent<LocomotionPeople>();
         clock = 0;

    }

    
    //触发器的消息一律委托给游戏控制器来发送。不再为触发器再单独建立Entity
    void OnTriggerEnter(Collider collider)
    {
        //如果检测到主席进入了触发区域，就播放挥手动画。
        clock = 0;
        if (collider.gameObject.tag == Tags.Leader)
        {
                PeopleAC.TurnOnSpot(collider.transform.position - this.gameObject.transform.position);
                int rand = Random.Range(0,10) ;
                
                //群众以不同的概率做动作：挥手 敬礼 鼓掌
                if (rand < 5)
                {
                   // animator.SetBool("WaveHand", true);
                    PeopleAC.WaveHand();
                }
                else if(rand < 8)
                {
                    //animator.SetBool("Salute", true);
                    PeopleAC.Salute();
                }
                
                else 
                {
                    //animator.SetBool("ClapHands",true);
                    PeopleAC.ClapHands();
                }
            //群众以很小的概率说话五分之一
            if(rand == 1 || rand == 2)
            {
                audiosource.Play();
            }
                //audiosource.Play();
        }
     

    }

    void OnTriggerStay(Collider collider)
    {
        clock += Time.deltaTime;
        if (collider.gameObject.tag == Tags.Leader)
        {
            this.gameObject.transform.LookAt(collider.transform);
            //PeopleAC.TurnOnSpot(collider.transform.position - this.gameObject.transform.position);
        }

        if (clock > 4.0f)
        {
            PeopleAC.StopClapHands();
            PeopleAC.StopSalute();
            PeopleAC.StopWaveHand();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        clock = 0;
        //如果检测到主席进入了触发区域，就播放挥手动画。
        if (collider.gameObject.tag == Tags.Leader)
        {

            audiosource.Stop();
            PeopleAC.StopClapHands();
            PeopleAC.StopSalute();
            PeopleAC.StopWaveHand();
            //animator.SetBool("WaveHand", false);
            //animator.SetBool("Salute", false);
            //animator.SetBool("ClapHands", false);

        }
    }

    public LocomotionPeople GetAnimatorController(){return PeopleAC;}
}

