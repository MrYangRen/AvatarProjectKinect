using UnityEngine;
using System.Collections;

public class Const : MonoBehaviour
{
    public const float SHOW_RANDTIP_POSSIBILITY = 0.6F;

    public const float INTERACTIVE_DISTANCE = 1.12f;
    public const float ARRIVED_DISTANCE = 0.2F;

    public const float FOLLOWER_REACT_TIME = 2.0F;
    public const float FOLLOWER_WARN_TIME = 1.0F;
    public const float FOLLOWER_CLAPHANDS_TIME = 1.5F;

    public const float LEADER_POSSIBILITY_OF_CLOSE_TO_PLAYER =1F;
    public const float LEADER_POSSIBILITI_TO_TALK = 0.5F;
    public const float LEADER_TALK_FREQUENCY = 10F;
    public const float LEADER_REACT_TIME = 1.0F;
    public const float LEADER_SALUTE_TIME = 3.0F;
    public const float LEADER_WAVEHAND_TIME = 3.0F;
    public const float LEADER_SHAKEHANDS_TIME = 2.042F;
    public const float LEADER_WAIT_TIME = 5.0F;
    public const float LEADER_WAIT_TIME_AFTER_SPEECH =5.0F;


    public const float PLAYER_SALUTE_TIME = 1.667F;
    public const float PLAYER_WAVEHAND_TIME = 2.267F;
    public const float PLAYER_SHAKEHANDS_TIME = 1.667F;
    public const float PLAYER_CLAPHANDS_TIME = 2.833F;
    public const float PLAYER_VOICE_LENGTH = 5.0F;

    //如果超过这个时间一直不鼓掌，就不检测鼓掌了
    public const float PLAYER_CLAPHANDS_WAIT_TIME = 6.0F;

    public const float PEOPLE_CLAPHANDS_TIME = 1.5F;

    public const float ONE_FREAME_TIME = 0.02F;

    public const float PLAYER_MIN_ROTATE_FREAME = 30F;

    public const float CAMERA_LOOK_HEIGHT = 10F;
}
