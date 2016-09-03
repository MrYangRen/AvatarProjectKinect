using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class Camera_MidSceneSideState : State<MyCamera>
{
    //拍摄中景（侧面）的状态：MidSceneSideState——在目标侧前方的一定偏移位置拍摄目标侧面，使得目标露出全身。目标前进则摄像机跟随（跟拍）
    //Ramdon at right of left
    [HideInInspector]
    public bool is_right = true;

    //the Mid Scene View Offset from The Leader
    public Vector3 mid_Scene_Offset;
    private Vector3 velocity_p = Vector3.zero;

    //Shockproof
    private Vector3 target_Pos1 = Vector3.zero;
    private Vector3 target_Pos2 = Vector3.zero;
    private Vector3 target_Pos3 = Vector3.zero;
    private Vector3 target_Pos4 = Vector3.zero;
    private Vector3 target_Pos5 = Vector3.zero;

    public float tracking_Speed;

    //Time Mark for ChangeState
    private float time_Accumulator = 0;
    public float time_Limit;

    public override void Enter(MyCamera camera)
    {
        time_Accumulator = 0;
        if (is_right) //如果在右边，把offset的x的符号变为相反数
        {
            mid_Scene_Offset = new Vector3(-mid_Scene_Offset.x, mid_Scene_Offset.y, mid_Scene_Offset.z);
        }
        target_Pos1 = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
        target_Pos2 = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
        target_Pos3 = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
        target_Pos4 = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
        target_Pos5 = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
    }

    public override void Execute(MyCamera camera)
    {
        Vector3 target_Pos = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
        if (Vector3.Dot(target_Pos5 - target_Pos1, target_Pos - target_Pos5) < 0.01f)
        {
            target_Pos = target_Pos2;
        }
        time_Accumulator += Time.deltaTime;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target_Pos, ref velocity_p, 1 / tracking_Speed);
        Quaternion euler1 = camera.transform.rotation;
        camera.transform.LookAt(new Vector3(camera.leader.position.x, Const.CAMERA_LOOK_HEIGHT, camera.leader.position.z));
        Quaternion euler2 = camera.transform.rotation;
        camera.transform.rotation = Quaternion.Lerp(euler1, euler2, Time.deltaTime);
        if (camera.getPlotSwitch()==false)
        {
            if (time_Accumulator > time_Limit)
            {
                camera.GetFSM().ChangeState(camera.States.FarSceneState);
            }
        }
        target_Pos1 = target_Pos2;
        target_Pos2 = target_Pos3;
        target_Pos3 = target_Pos4;
        target_Pos4 = target_Pos5;
        target_Pos5 = camera.leader.position + camera.leader.rotation * mid_Scene_Offset;
    }

    public override void Exit(MyCamera camera)
    {

    }

    public override bool OnMessage(MyCamera camera, MessageEnum msg)
    {
        switch (msg)
        {
            case MessageEnum.StartSpeech: camera.GetFSM().ChangeState(camera.States.LeaderViewState); break;
        }
        return true;
    }
}