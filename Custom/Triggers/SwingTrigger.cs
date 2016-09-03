using UnityEngine;
using System.Collections;

public class SwingTrigger : MonoBehaviour {

    SecondCamera secondCamera;
    public Transform SwingLookTarget;
    public GameObject targetHuman;
    public

    void Start()
    {
        secondCamera = GameObject.FindGameObjectWithTag(Tags.SecondCamera).GetComponent<SecondCamera>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Leader)
        {
            secondCamera.SetTarget(SwingLookTarget);
            if (targetHuman != null)
            {
                float rand = Random.Range(0, 10);
                if (rand < 3.0f)
                {
                    targetHuman.GetComponent<LocomotionPeople>().WaveHand();
                }
                else if (rand < 6.0f)
                {
                    targetHuman.GetComponent<LocomotionPeople>().ClapHands();
                }
                else
                {
                    targetHuman.GetComponent<LocomotionPeople>().Salute();
                }
                targetHuman.GetComponent<AudioSource>().Play();
            }
            secondCamera.Swing();
        }
    }

}
