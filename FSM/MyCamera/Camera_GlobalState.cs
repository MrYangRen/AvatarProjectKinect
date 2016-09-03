using UnityEngine;
using System.Collections;

public class Camera_GlobalState : State<MyCamera>
{
    public override void Enter(MyCamera camera)
    {

    }

    public override void Execute(MyCamera camera)
    {
        //如果没有进入固定剧情
        if (camera.getPlotSwitch() == false)
        {
            //如果出现玩家且目前照相机还是按照固定路线拍摄 就转换到PlayerViewState
            if (GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer()
                && (camera.GetFSM().CurrentState() == camera.States.FarSceneState ||
                camera.GetFSM().CurrentState() == camera.States.MidSceneFrontState ||
                camera.GetFSM().CurrentState() == camera.States.MidSceneSideState))
            {
                camera.GetFSM().ChangeState(camera.States.PlayerViewState);
            }
            //如果没有玩家且目前照相机是在有玩家才能进行的三个状态中 就转换到FarSceneState
            else if (!GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>().getAlreadyHasPlayer()
                && (camera.GetFSM().CurrentState() == camera.States.PlayerViewState ||
                camera.GetFSM().CurrentState() == camera.States.PlayerLeaderFeatureState ||
                camera.GetFSM().CurrentState() == camera.States.CloseSceneState))
            {
                camera.GetFSM().ChangeState(camera.States.FarSceneState);
            }

        }
        //如果进入了固定剧情。就先转换到侧拍状态
        else
        {
            if (camera.GetFSM().CurrentState() != camera.States.MidSceneSideState && camera.GetFSM().CurrentState()!=camera.States.LeaderViewState)
            {
                camera.GetFSM().ChangeState(camera.States.MidSceneSideState);
            }
        }
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
