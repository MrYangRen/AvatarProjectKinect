using UnityEngine;
using System.Collections;

public class BGM {
    private static BGM _instance;
    public static BGM Instance() 
    {
        if (_instance == null) { return new BGM(); }
        else { return _instance; }
    }

    private AudioSource bgm;

    public void FadeInBGM() 
    {
        bgm = GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<AudioSource>();
        bgm.volume = 0.2f;
 
    }

    public void FadeOutBGM()
    {
        bgm = GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<AudioSource>();
        bgm.volume = 0.0f;
    }

}
