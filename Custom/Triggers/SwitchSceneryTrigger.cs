using UnityEngine;
using System.Collections;

public class SwitchSceneryTrigger : MonoBehaviour {

    SecondCamera secondCamera;
    public Transform referencePoint;
    
    void Start()
    {
        secondCamera = GameObject.FindGameObjectWithTag(Tags.SecondCamera).GetComponent<SecondCamera>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Leader)
        {
            secondCamera.SetRegerencePoint(referencePoint);
            secondCamera.Still();
        }
    }




}
