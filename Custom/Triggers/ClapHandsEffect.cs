using UnityEngine;
using System.Collections;

public class ClapHandsEffect {

    private static ClapHandsEffect _instance;
    public static ClapHandsEffect Instance() { if (_instance == null)return new ClapHandsEffect(); else return _instance; }
    private AudioSource audio;
    public void StartClapHandsEffet() 
    {
        audio = GameObject.Find("ClapHandsEffect").GetComponent<AudioSource>();
        audio.enabled = true;
    }
    public void StopClapHandsEffet()
    {
        audio = GameObject.Find("ClapHandsEffect").GetComponent<AudioSource>();
        audio.enabled = false;
    }

}
