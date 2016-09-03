using UnityEngine;
using System.Collections;

public enum SecondCameraState
{
    Still,
    Swing,
    none
}

public class SecondCamera : MonoBehaviour {

    private GameObject MainCamera;
    
    private Vector3 velocity_p = Vector3.zero;
    public float swingSpeed = 2.0f;
    public float swingLastingTime = 5.0f;

    private Transform referencePoint;
    private Transform target;

    private SecondCameraState state;
    private float swingTimer = 0.0f;
    private Vector3 target_LookAt;
    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag(Tags.Camera);
        state = SecondCameraState.none;
    }

    void Update()
    {
        if (state == SecondCameraState.Still )
        {
            this.GetComponent<Camera>().enabled = true;
            Vector3 targetPos = referencePoint.position;
            Vector3 targetLookAtPos = referencePoint.position + referencePoint.forward;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity_p, 1 / moveSpeed);
            transform.position = targetPos;
            //Quaternion euler1 = transform.rotation;
            transform.LookAt(targetLookAtPos);
            //Quaternion euler2 = transform.rotation;
            //transform.rotation = Quaternion.Slerp(euler1, euler2, Time.deltaTime * rotateSpeed);
        }
        else if (state == SecondCameraState.Swing)
        {
            this.GetComponent<Camera>().enabled = true;
            transform.position = target.position;
            transform.LookAt(target_LookAt);
            swingTimer += Time.deltaTime;
            if (swingTimer < swingLastingTime)
            {
                target_LookAt += transform.InverseTransformVector(new Vector3(Time.deltaTime * swingSpeed, 0f, 0f));
            }
            else
            { state = SecondCameraState.none; }
        }
        else
        {
            swingTimer = 0.0f;
            this.GetComponent<Camera>().enabled = false;
            transform.position = MainCamera.transform.position;
            transform.rotation = MainCamera.transform.rotation;
        }

    }


    public void Still() { state = SecondCameraState.Still; MainCamera.GetComponent<Camera>().enabled = false; }
    public void Swing() { state = SecondCameraState.Swing; MainCamera.GetComponent<Camera>().enabled = true; }

    public void SetTarget(Transform _target) { target = _target; target_LookAt = target.position + target.forward * 2; }
    public void SetRegerencePoint(Transform reference) { referencePoint = reference; }
}
