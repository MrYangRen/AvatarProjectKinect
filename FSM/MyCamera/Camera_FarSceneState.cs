using UnityEngine;
using System.Collections;

public class Camera_FarSceneState : State<MyCamera>
{
    #region 成员变量
    //摇拍远景的状态FarSceneState——在远处的某一个点不动拍摄目标（旋转摄像头摇拍）
    //the Far Scene View Offset from The Leader
    public Vector3 far_Scene_Offset;
    private Vector3 velocity_p = Vector3.zero;

    //Shockproof
    private Vector3 target_Pos1 = Vector3.zero;
    private Vector3 target_Pos2 = Vector3.zero;

    public float tracking_Speed;

    //Time Mark for ChangeState
    private float time_Accumulator = 0;
    public float time_Limit=10f;


    private bool hasSendMessage=false;
    private const float delayTime = 3.0f;
    #endregion


    #region override
    public override void Enter(MyCamera camera)
    {
        time_Accumulator = 0;
        target_Pos1 = camera.leader.position + camera.leader.rotation * far_Scene_Offset;
        target_Pos2 = camera.leader.position + camera.leader.rotation * far_Scene_Offset;
    }

    public override void Execute(MyCamera camera)
    {

        Vector3 target_Pos = camera.leader.position + camera.leader.rotation * far_Scene_Offset;
        if (Vector3.Dot(target_Pos2 - target_Pos1, target_Pos - target_Pos2) < 0.01f)
        {
            target_Pos = target_Pos2;
        }
        time_Accumulator += Time.deltaTime;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target_Pos, ref velocity_p, 1 / tracking_Speed);
        Quaternion euler1 = camera.transform.rotation;
        camera.transform.LookAt(new Vector3(camera.leader.position.x, Const.CAMERA_LOOK_HEIGHT, camera.leader.position.z));
        Quaternion euler2 = camera.transform.rotation;
        camera.transform.rotation = Quaternion.Slerp(euler1, euler2, Time.deltaTime);

        if (time_Accumulator > time_Limit)
        {
            if (Random.value < 0.3f)
            {//转到右边
                camera.GetFSM().ChangeState(camera.States.MidSceneSideState);
                camera.States.MidSceneSideState.is_right = true;
                //camera.transform.FindChild("CameraStates").GetComponent<Camera_MidSceneSideState>().is_right = true;
            }
            else if (Random.value > 0.7f)
            {//转到左边
                camera.GetFSM().ChangeState(camera.States.MidSceneSideState);
                camera.States.MidSceneSideState.is_right = false;
                //camera.transform.FindChild("CameraStates").GetComponent<Camera_MidSceneSideState>().is_right = false;
            }
            else
            {//转到前面
                camera.GetFSM().ChangeState(camera.States.MidSceneFrontState);
            }
        }
        if (!hasSendMessage && time_Accumulator > delayTime)
        {
            hasSendMessage = true;
            MessageDispatcher.Instance().DisparcherMessage(0.0f,camera.ID(), camera.leader.gameObject.GetComponent<Leader>().ID(), MessageEnum.StartWalking);
 
        }
        target_Pos1 = target_Pos2;
        target_Pos2 = camera.leader.position + camera.leader.rotation * far_Scene_Offset;
    }

    public override void Exit(MyCamera camera)
    {

    }

    public override bool OnMessage(MyCamera camera, MessageEnum msg)
    {
        return true;
    }
    #endregion
}
