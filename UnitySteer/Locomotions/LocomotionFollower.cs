/// <summary>
/// 
/// </summary>
using UnityEngine;
using UnitySteer.Behaviors;
using System.Collections;

public class LocomotionFollower : MonoBehaviour
{

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private float previousSpeed;
    private float previousDirection;
    private float currentDirection;
    private float currentSpeed;
    private Locomotion locomotion;
    private Vehicle vehicle;
    private Vector3 relativePosition;
    private Vector3 previousForward;
    private Transform leader;
    private float LowDownSpeed = 0.1f;
    private int accelerate = 2;
    private int changeSpeed = 2;


    // Use this for initialization
    void Awake()
    {
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).transform;
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
        vehicle = GetComponent<Vehicle>();
    }

    public void StopWalking()
    {
        this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        this.gameObject.GetComponent<SteerForAvoid>().enabled = true;
        speed = vehicle.Speed;
        if (speed != 0f)
        {
            Vector3 axis = Vector3.Cross(transform.forward, leader.transform.position - transform.position);
            direction = Vector3.Angle(transform.forward, leader.transform.position - transform.position) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        else
        {
            Vector3 axis = Vector3.Cross(vehicle.Velocity, vehicle.DesiredVelocity);
            direction = Vector3.Angle(vehicle.Velocity, vehicle.DesiredVelocity) / 180.0f * (axis.y < 0 ? -1 : 1);
            if (vehicle.Velocity == Vector3.zero && vehicle.DesiredVelocity == Vector3.zero)
                direction = 0f;
        }
        locomotion.Do(speed * 6, direction * 180);
    }

    public void Walking()
    {
        speed = vehicle.Speed;
        //再根据是否是在主席的前方来处理速度的情况
        relativePosition = transform.position - leader.transform.position;
        //people在主席前方的情况
        if (Vector3.Dot(leader.transform.forward, relativePosition.normalized) > 0.4)
        {
            this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
            this.gameObject.GetComponent<SteerForAvoid>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = true;
            this.gameObject.GetComponent<SteerForAvoid>().enabled = false;
            //朝向的更新，反正朝向的突变
            //transform.forward = Vector3.Slerp(previousForward, transform.forward, Time.deltaTime * 20);
            //previousForward = transform.forward;
        }

        if (Vector3.Distance(transform.position, leader.position) < 2f)
        {
            speed = LowDownSpeed;
        }
        else if (Vector3.Distance(transform.position, leader.position) > 5f)
        {
            speed *= accelerate;
        }


        if (speed >= 0.01f)
        {
            Vector3 axis = Vector3.Cross(vehicle.Velocity, vehicle.DesiredVelocity);
            direction = Vector3.Angle(vehicle.Velocity, vehicle.DesiredVelocity) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        if (vehicle.Velocity == Vector3.zero && vehicle.DesiredVelocity == Vector3.zero)
            direction = 0f;

        if (direction * 180 > 15 || direction * 180 < -15)
            direction = 0.006f;
        
        currentDirection = Mathf.Lerp(previousDirection, direction, Time.deltaTime*changeSpeed);

        currentSpeed = Mathf.Lerp(previousSpeed, speed, Time.deltaTime*changeSpeed);

        locomotion.Do(currentSpeed * 6, currentDirection * 180);

        previousDirection = currentDirection;
        previousSpeed = currentSpeed;
    }

    //主席各种上半身的动作
    public void ShakeHands()
    {
        animator.SetBool("ShakeHands", true);
    }

    public void StopShakeHands()
    {
        animator.SetBool("ShakeHands", false);
    }

    public void WaveHand()
    {
        animator.SetBool("WaveHand", true);
    }

    public void StopWaveHand()
    {
        animator.SetBool("WaveHand", false);
    }

    public void Salute()
    {
        animator.SetBool("Salute", true);
    }

    public void StopSalute()
    {
        animator.SetBool("Salute", false);
    }
    public void ClapHands()
    {
        animator.SetBool("ClapHands", true);
    }

    public void StopClapHands()
    {
        animator.SetBool("ClapHands", false);
    }

    public bool GetBoolWaveHand()
    {
        return animator.GetBool("WaveHand");
    }

}
