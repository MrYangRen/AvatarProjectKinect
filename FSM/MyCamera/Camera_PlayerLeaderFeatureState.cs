using UnityEngine;
using System.Collections;

public class Camera_PlayerLeaderFeatureState : State<MyCamera>
{
    //拍摄玩家和主席交互的相机状态
    private Vector3 velocity_p = Vector3.zero;

    public float tracking_Speed;
    public float plusHeight = -1f;
    public float distance = 0;

    public override void Enter(MyCamera camera)
    {

    }

    public override void Execute(MyCamera camera)
    {
        Vector3 look_Pos = (camera.leader.position + camera.player.position) / 2;
        Vector3 target_Pos = look_Pos + Quaternion.Euler(0, 90, 0) 
            * (camera.leader.position - camera.player.position).normalized
            * Vector3.Distance(camera.leader.position, camera.player.position) * distance;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, new Vector3(target_Pos.x, Const.CAMERA_LOOK_HEIGHT + plusHeight, target_Pos.z), ref velocity_p, 1 / tracking_Speed);
        Quaternion euler1 = camera.transform.rotation;
        camera.transform.LookAt(new Vector3(look_Pos.x, Const.CAMERA_LOOK_HEIGHT + plusHeight, look_Pos.z));
        Quaternion euler2 = camera.transform.rotation;
        camera.transform.rotation = Quaternion.Lerp(euler1, euler2, Time.deltaTime);

    }

    public override void Exit(MyCamera camera)
    {

    }

    public override bool OnMessage(MyCamera camera, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.LeaderStopInteractive: camera.GetFSM().ChangeState(camera.States.PlayerViewState); break;
        }
        return true;
    }
}
