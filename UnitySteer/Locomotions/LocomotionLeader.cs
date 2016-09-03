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
public class LocomotionLeader : MonoBehaviour {

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private float previousDirection;
    private float previousSpeed;
    private float currentDirection;
    private float currentSpeed;
    private Locomotion locomotion = null;
    private Vehicle vehicle;
    private int changeSpeed = 2;
    

   
	// Use this for initialization
    void Awake() 
	{
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
        vehicle = GetComponent<Vehicle>();
	}

    //每一帧都调用才能让主席进行移动
    public void Move() {
        speed = vehicle.Speed;
        if (speed >= 0.01f)
        {
            Vector3 axis = Vector3.Cross(vehicle.Velocity, vehicle.DesiredVelocity);
            direction = Vector3.Angle(vehicle.Velocity, vehicle.DesiredVelocity) / 180.0f * (axis.y < 0 ? -1 : 1);
            previousDirection = direction;
        }
        currentDirection = Mathf.Lerp(previousDirection, direction, Time.deltaTime*changeSpeed);

        currentSpeed = Mathf.Lerp(previousSpeed, speed, Time.deltaTime*changeSpeed);

        locomotion.Do(currentSpeed * 6, currentDirection * 180);

        previousDirection = currentDirection;
        previousSpeed = currentSpeed;
    }

    //调用一次就可以完成转身到targetDirection,但是控制脚本中要检测是否已经转身完成再调用Stop()让主席停下来
    public void TurnOnSpot(Vector3 targetDirection)
    {
        Vector3 axis = Vector3.Cross(transform.forward, targetDirection);
        direction = Vector3.Angle(transform.forward, targetDirection) / 180.0f * (axis.y < 0 ? -1 : 1);
        if (Mathf.Abs(direction * 180) > 2)
        {
            locomotion.Do(0.0f, direction * 180);
            //animator.SetFloat("Speed", 0.05f);
            //animator.SetFloat("Direction", direction * 180);

        }
    }

    public void Stop() {
        speed = 0.0f;
        direction =0.0f;
        locomotion.Do(speed * 6, direction * 180);
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

    public void MaskWaveHand()
    {
        animator.SetBool("MaskWaveHand", true);
    }

    public void StopMaskWaveHand()
    {
        animator.SetBool("MaskWaveHand", false);
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
}
