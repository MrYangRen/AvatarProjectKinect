 /// <summary>
/// 
/// </summary>
using UnityEngine;
using UnitySteer.Behaviors;
using System.Collections;

public class LocomotionPlayer : MonoBehaviour
{

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private Locomotion locomotion = null;
    private Vehicle vehicle;
    private Vector3 relativePosition;
    private Transform leader;
    private float LowDownSpeed = 0.2f;
    private int accelerate = 3;


    // Use this for initialization
    void Awake()
    {
        leader = GameObject.FindGameObjectWithTag("Leader").transform;
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
        vehicle = GetComponent<Vehicle>();
    }

    //每一帧都调用才能让主席进行移动
    public void Move()
    {
        speed = vehicle.Speed;
        if (speed >= 0.01f)
        {
            Vector3 axis = Vector3.Cross(vehicle.Velocity, vehicle.DesiredVelocity);
            direction = Vector3.Angle(vehicle.Velocity, vehicle.DesiredVelocity) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        locomotion.Do(speed * 6, direction * 180);
    }

    //调用一次就可以完成转身到targetDirection,但是控制脚本中要检测是否已经转身完成再调用Stop()让主席停下来
    public void TurnOnSpot(Vector3 targetDirection)
    {
        Vector3 axis = Vector3.Cross(transform.forward, targetDirection);
        direction = Vector3.Angle(transform.forward, targetDirection) / 180.0f * (axis.y < 0 ? -1 : 1);
        if (Mathf.Abs(direction * 180) > 10)
        {
            locomotion.Do(0.0f, direction * 180);

        }
    }

    public void Stop()
    {
        this.gameObject.GetComponent<SteerForNeighborGroup>().enabled = false;
        this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = false;
        speed = 0.0f;
        direction = 0.0f;
        locomotion.Do(speed * 6, direction * 180);
    }

    public void FollowLeader()
    {
        speed = vehicle.Speed;
        this.gameObject.GetComponent<SteerForNeighborGroup>().enabled = true;
        this.gameObject.GetComponent<SteerForLeadFollowing>().enabled = true;

        //先根据距离主席距离的远近来处理速度的情况
        if (Vector3.Distance(transform.position, leader.position) < 0.5f)
        {
            speed = LowDownSpeed;
        }
        else if (Vector3.Distance(transform.position, leader.position) > 9f)
        {
            speed *= accelerate;
        }


        if (speed >= 0.01f)
        {
            Vector3 axis = Vector3.Cross(vehicle.Velocity, vehicle.DesiredVelocity);
            direction = Vector3.Angle(vehicle.Velocity, vehicle.DesiredVelocity) / 180.0f * (axis.y < 0 ? -1 : 1);
        }
        //避免出现瞬时的转动，大部分是在-20到20之间，突然出现30,40的会有抽搐
        if (direction * 180 > 20 || direction * 180 < -20)
            direction = 0.1f;
        locomotion.Do(speed * 6, direction * 180);
    }



    #region 各种上半身的动作
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
#endregion

}
