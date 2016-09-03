using UnityEngine;
using System.Collections;

public class NPCWaveHands : MonoBehaviour {

    
    public AudioSource audiosource;
    private Leader leader;
    private LocomotionPeople PeopleAC;

    void Start()
    {
       
        audiosource = this.gameObject.GetComponent<AudioSource>();
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).GetComponent<Leader>();
        PeopleAC = this.gameObject.GetComponent<LocomotionPeople>();

    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Leader)
        {
            PeopleAC.TurnOnSpot(collider.transform.position - this.gameObject.transform.position);
            PeopleAC.WaveHand();
            int rand = Random.Range(0, 10);
            if(rand < 6)
            audiosource.Play();
        }

    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Leader)
        {

            PeopleAC.WaveHand();
        }

    }


  
}
