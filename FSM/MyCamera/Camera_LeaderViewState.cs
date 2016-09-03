using UnityEngine;
using System.Collections;

public class Camera_LeaderViewState : State<MyCamera>
{
    //拍摄主席视角的状态，当主席演讲的时候需要转换到这个状态
    //the Far Scene View Offset from The Leader
    public Vector3 leader_View_Scene_Offset;
    private Vector3 velocity_p = Vector3.zero;
    public float tracking_Speed;

    public override void Enter(MyCamera camera)
    {
    }

    public override void Execute(MyCamera camera)
    {
        Vector3 lookPos = camera.leader.position + camera.leader.forward;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, camera.leader.position + camera.leader.rotation * leader_View_Scene_Offset, ref velocity_p, 1 / tracking_Speed);
        Quaternion euler1 = camera.transform.rotation;
        camera.transform.LookAt(new Vector3(lookPos.x, Const.CAMERA_LOOK_HEIGHT, lookPos.z));
        Quaternion euler2 = camera.transform.rotation;
        camera.transform.rotation = Quaternion.Lerp(euler1, euler2, Time.deltaTime);
        //camera.transform.LookAt(new Vector3(camera.leader.position.x, 1.5f, camera.leader.position.z));
    }

    public override void Exit(MyCamera camera)
    {

    }

    public override bool OnMessage(MyCamera camera, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderStopSpeech: camera.GetFSM().ChangeState(camera.States.MidSceneSideState); break;
        }
        return true;
    }
}