using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnitySteer.Behaviors;



public class MyAnimatorController : MonoBehaviour
{
    Vehicle vehicle;
    Animator ani;

	void Start () {
        ani = GetComponent<Animator>();
        vehicle = gameObject.GetComponent<Vehicle>();
	}
	

	void Update () {
        setSpeed(vehicle.Speed);

	}

    public void setSpeed(float speed)
    {
        ani.SetFloat("Speed", speed);
    }
    public void setAngularSpeed(float angularSpeed)
    {
        ani.SetFloat("AngularSpeed", angularSpeed);
    }
    public void setIsMove(bool flag)                                                                                         
    {
        ani.SetBool("IsMove", flag);
    }


    public void PlayWaveHandsAnim()
    {
        ani.SetBool("IsWaveHands", true);
    }

    public void StopWaveHandsAnim()
    {
        ani.SetBool("IsWaveHands", false);
    }

    public void PlayShakeHandsAnim()
    {
        ani.SetBool("IsShakeHands", true);
    }

    public void StopShakeHandsAnim()
    {
        ani.SetBool("IsShakeHands", false);
    
    }

    public void PlaySaluteAnim()
    {
        ani.SetBool("IsSalute", true);
    }

    public void StopSaluteAnim()
    {
        ani.SetBool("IsSalute", false);
    }

}
