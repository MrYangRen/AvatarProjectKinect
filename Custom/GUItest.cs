using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
public class GUItest : MonoBehaviour {


    public static bool playerEnterIn;
    public static bool playerEnterWrong;
    public static bool playerExit;
    public static bool playerWaveHands;
    public static bool playerSalute;
    public static bool playerShakeHands;
    public static bool playerClapHands;
    public static bool startWalking;
    public static bool stopWalking;
    public static bool startSpeech;
    public static bool playerWalk;
    public GameObject UIRoot;
    public Massage_Enum kinectMessage;
    public Massage_Enum lastKinectMessage;
    private float timer = 0.0f;
    
    
    void Start()
    {
        kinectMessage = gameObject.GetComponent<KinectWrapper2>().massage;
        lastKinectMessage = Massage_Enum.NOPLAYER;
        kinectMessage = Massage_Enum.NOPLAYER;
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(40);
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
        //if (GUILayout.Button("玩家出现"))
        //{
        //    playerEnterIn = true;
        //}
        //else
        //{
        //    playerEnterIn = false;
        //}
        //if (GUILayout.Button("玩家退出"))
        //{
        //    playerExit = true;
        //}
        //else
        //{
        //    playerExit = false;
        //}

    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    playerWaveHands = true;
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    playerWaveHands = false;
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    playerShakeHands = true;
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    playerShakeHands = false;
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    playerSalute = true;
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    playerSalute = false;
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    playerClapHands = true;
        //}
        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    playerClapHands = false;
        //}


        timer += Time.deltaTime;
        if (timer > 4.0f)
        {

            kinectMessage = gameObject.GetComponent<KinectWrapper2>().massage;


            if (kinectMessage == Massage_Enum.NONE && lastKinectMessage != Massage_Enum.NONE)
            {
                playerEnterIn = true;
            }
            else
            {
                playerEnterIn = false;
            }


            if (kinectMessage == Massage_Enum.OUTCAMERA && lastKinectMessage != Massage_Enum.OUTCAMERA)
            {
                playerEnterWrong = true;
            }
            else
            {
                playerEnterWrong = false;

            }
            if (kinectMessage == Massage_Enum.NOPLAYER && lastKinectMessage != Massage_Enum.NOPLAYER)
            {
                playerExit = true;
            }
            else
            {
                playerExit = false;
            }

            if (kinectMessage == Massage_Enum.FORWARD && lastKinectMessage != Massage_Enum.FORWARD)
            {
                playerWalk = true;
            }
            else
            {
                playerWalk = false;
            }


            if (kinectMessage == Massage_Enum.WAVE)
            {
                playerWaveHands = true;
            }
            else
            { playerWaveHands = false; }


            if (kinectMessage == Massage_Enum.SALUTE)
            {
                playerSalute = true;
            }
            else
            { playerSalute = false; }


            if (kinectMessage == Massage_Enum.SHAKE)
            {
                playerShakeHands = true;
            }
            else
            { playerShakeHands = false; }


            if (kinectMessage == Massage_Enum.CLAP)
            {
                playerClapHands = true;
            }
            else
            { playerClapHands = false; }




            lastKinectMessage = kinectMessage;
        }

    }
}
