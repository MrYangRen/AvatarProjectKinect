/// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using UnitySteer.Behaviors;
using System.Collections;

[RequireComponent(typeof(Animator))]

//Name of class must be name of file as well


//用动画控制主席的运动
public class LocomotionPeople : MonoBehaviour
{

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private float previousSpeed;
    private float previousDirection;
    private float currentDirection;
    private float currentSpeed;
    private float smoothTime = 5f;
    private Vector3 previousForward;
    private Vector3 velocity_p = Vector3.zero;

    private Locomotion locomotion;
    private Vehicle vehicle;
    private Vector3 relativePositionToLeader;
    private Vector3 relativePositionToFollower;
    private Transform leader;
    private Transform follower;
    private float LowDownSpeed = 0.1f;
    private int accelerate = 3;
    private int changeSpeed = 2;
    



    // Use this for initialization
    void Awake()
    {
        follower = GameObject.FindGameObjectWithTag(Tags.Follower).transform;
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).transform;
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
        vehicle = GetComponent<Vehicle>();
    }


    public void StopWalking()
    {
        this.gameObject.GetComponent<SteerForNeighborGroup>().enabled = false;
        this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        this.gameObject.GetComponent<SteerForEvasion>().enabled = true;
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
            //当都为0的时候，animator会将direction自动设置为90，因此要置零
            if (vehicle.Velocity == Vector3.zero && vehicle.DesiredVelocity == Vector3.zero)
                direction = 0f;
        }

        locomotion.Do(speed * 6, direction * 180);
    }

    public void Walking()
    {
        speed = vehicle.Speed;
        this.gameObject.GetComponent<SteerForNeighborGroup>().enabled = true;
        //再根据是否是在主席的前方来处理速度的情况
        relativePositionToLeader = transform.position - leader.transform.position;
        relativePositionToFollower = transform.position - follower.transform.position;
        //people在主席前方的情况
        if (Vector3.Dot(leader.transform.forward, relativePositionToLeader.normalized) > 0.2)
        {
            this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
            this.gameObject.GetComponent<SteerForEvasion>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = true;
            this.gameObject.GetComponent<SteerForEvasion>().enabled = false;
            //朝向的更新，反正朝向的突变,躲避的时候不做平滑处理
            //transform.forward = Vector3.Slerp(previousForward, transform.forward, Time.deltaTime * 30);
            //previousForward = transform.forward;
            
        }


        if (Vector3.Dot(follower.transform.forward, relativePositionToFollower.normalized) > 0.1)
        {
            this.gameObject.GetComponent<SteerForAvoid>().enabled = true;
        }
        else
            this.gameObject.GetComponent<SteerForAvoid>().enabled = false;


        //先根据距离主席距离的远近来处理速度的情况
        if (Vector3.Distance(transform.position, leader.position) < 1f)
        {
            speed = LowDownSpeed;
        }
        else if (Vector3.Distance(transform.position, leader.position) > 6f)
        {
            speed *= accelerate;
        }


        if (speed >= 0.01f)
        {
            Vector3 axis = Vector3.Cross(vehicle.Velocity, vehicle.DesiredVelocity);
            direction = Vector3.Angle(vehicle.Velocity, vehicle.DesiredVelocity) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        //避免出现瞬时的转动，大部分是在-20到20之间，突然出现30,40的会有抽搐
        if (direction * 180 > 15 || direction * 180 < -15)
            direction = 0.006f;

        currentDirection = Mathf.Lerp(previousDirection, direction, Time.deltaTime*changeSpeed);

        currentSpeed = Mathf.Lerp(previousSpeed, speed, Time.deltaTime*changeSpeed);

        locomotion.Do(currentSpeed * 6, currentDirection * 180);

        previousDirection = currentDirection;
        previousSpeed = currentSpeed;
    }

    public bool TurnOnSpot(Vector3 targetDirection)
    {
        Vector3 axis = Vector3.Cross(transform.forward, targetDirection);
        direction = Vector3.Angle(transform.forward, targetDirection) / 180.0f * (axis.y < 0 ? -1 : 1);
        float relative = Vector3.Dot(targetDirection.normalized, transform.forward);
        if (relative > 0.95 || Mathf.Abs(direction * 180) < 30)
        {
            //Debug.Log("主席转身已完成！");
            //StopWalking();
            //Debug.Log("true");
            return true;
        }
        locomotion.Do(0.0f, direction * 180);
        return false;
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

    public bool GetBoolWaveHand()
    {
        return animator.GetBool("WaveHand");
    }

    public float getDirection() { return direction; }

    public float getSpeed() { return speed; }

    public void ClapHands()
    {
        animator.SetBool("ClapHands", true);
       // Debug.Log("I CLAPING HANDS");
    }

    public void StopClapHands()
    {
        animator.SetBool("ClapHands", false);
        //Debug.Log("aaaaaaaa");
    }
}
