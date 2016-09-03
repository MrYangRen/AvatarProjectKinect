using UnityEngine;
using System.Collections;

public class Camera_CloseSceneState : State<MyCamera>
{
    //拍摄近景的状态：CloseSceneState——在目标前方的一定偏移位置拍摄目标正面，使得目标露出上半身。目标前进则摄像机后退（拉拍）

    private Vector3 velocity_p = Vector3.zero;
    public float Mutiple_Of_PlayerLeaderDist = 1.5f;
    public float plusHeight = 2.0f;

    public float tracking_Speed;
    //当两个人的距离是可交互距离+2的时候就转换到看向2人的交互状态，这样可以看到他们走进的过程
    public float dis_Can_Turn = Const.INTERACTIVE_DISTANCE + 2.0f;
    public override void Enter(MyCamera camera)
    {

    }

    public override void Execute(MyCamera camera)
    {
        Vector3 look_Pos = (camera.leader.position + camera.player.position) / 2;
        Vector3 target_Pos = look_Pos + Quaternion.Euler(0, 90, 0) * (camera.leader.position - camera.player.position).normalized * Vector3.Distance(camera.leader.position, camera.player.position) * Mutiple_Of_PlayerLeaderDist;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, new Vector3(target_Pos.x, Const.CAMERA_LOOK_HEIGHT + plusHeight, target_Pos.z), ref velocity_p, 1 / tracking_Speed);
        Quaternion euler1 = camera.transform.rotation;
        camera.transform.LookAt(look_Pos);
        Quaternion euler2 = camera.transform.rotation;
        camera.transform.rotation = Quaternion.Lerp(euler1, euler2, 0.1f);
        if (Vector3.Distance(camera.leader.position, camera.player.position) < dis_Can_Turn)
        {
            camera.GetFSM().ChangeState(camera.States.PlayerLeaderFeatureState);
        }
    }

    public override void Exit(MyCamera camera)
    {

    }

    public override bool OnMessage(MyCamera camera, MessageEnum msg)
    {
        return true;
    }
}