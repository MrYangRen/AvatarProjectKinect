using UnityEngine;
using System.Collections;

public class Camera_PlayerViewState : State<MyCamera>
{
    //玩家视角的相机状态
    private Vector3 velocity_p = Vector3.zero;

    public float tracking_Speed;

    public Vector3 offset_relate_to_Player;

    public override void Enter(MyCamera camera)
    {
        if (offset_relate_to_Player == Vector3.zero)
            offset_relate_to_Player = new Vector3(0, 2, -1);
    }

    public override void Execute(MyCamera camera)
    {

        //在玩家上后方
        Vector3 target_Pos = camera.player.position
            + offset_relate_to_Player.x*camera.player.right
            +offset_relate_to_Player.z*camera.player.forward
            +offset_relate_to_Player.y*camera.player.up;
        
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target_Pos, ref velocity_p, 1 / tracking_Speed);
        Quaternion euler1 = camera.transform.rotation;

        //看着玩家朝向的前方一点
        //Camera should look at where player look at
        Vector3 target_Look_Pos = camera.player.forward * 2 + camera.player.position;
        camera.transform.LookAt(new Vector3(target_Look_Pos.x, Const.CAMERA_LOOK_HEIGHT, target_Look_Pos.z));
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
            case MessageEnum.LeaderCloseToPlayer: camera.GetFSM().ChangeState(camera.States.CloseSceneState); break;
        }
        return true;
    }
}
