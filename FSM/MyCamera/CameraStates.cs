using UnityEngine;
using System.Collections;

public class CameraStates : MonoBehaviour
{
    [HideInInspector]
    public Camera_FarSceneState FarSceneState;
    [HideInInspector]
    public Camera_PlayerViewState PlayerViewState;
    [HideInInspector]
    public Camera_MidSceneSideState MidSceneSideState;
    [HideInInspector]
    public Camera_MidSceneFrontState MidSceneFrontState;
    [HideInInspector]
    public Camera_LeaderViewState LeaderViewState;
    [HideInInspector]
    public Camera_PlayerLeaderFeatureState PlayerLeaderFeatureState;
    [HideInInspector]
    public Camera_CloseSceneState CloseSceneState;

    [HideInInspector]
    public Camera_GlobalState GlobalState;
    

    void Awake()
    {
        FarSceneState = this.GetComponent<Camera_FarSceneState>();
        PlayerViewState = this.GetComponent<Camera_PlayerViewState>();
        MidSceneSideState = this.GetComponent<Camera_MidSceneSideState>();
        MidSceneFrontState = this.GetComponent<Camera_MidSceneFrontState>();
        LeaderViewState = this.GetComponent<Camera_LeaderViewState>();
        PlayerLeaderFeatureState = this.GetComponent<Camera_PlayerLeaderFeatureState>();
        CloseSceneState = this.GetComponent<Camera_CloseSceneState>();

        GlobalState = this.GetComponent<Camera_GlobalState>();
    }
}
