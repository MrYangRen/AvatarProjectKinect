using UnityEngine;
using System.Collections;


//这个脚本用来专门处理提示信息的动画
public class TipAnimation : MonoBehaviour
{

    #region 成员变量

    GameControllerState gameControllerState;
    public GameObject UIRoot;
    bool hasPlayerEnterTip = false;
    bool hasPlayerEnterWrong = false;
    bool hasPlayerExitTip = false;
    bool hasCanShakeHandsTip = false;
    bool hasCanClapHandsTip = false;
    bool hasRandTip1 = false;
    bool hasRandTip2 = false;
    
    float ShowTipTime = 3.0f;
    float PlayerEnterTipExistTimer = 0.0f;
    float PlayerEnterWrongTipExistTimer = 0.0f;
    float PlayerExitTipExistTimer = 0.0f;
    float CanShakeHandsTipExistTimer = 0.0f;
    float CanClapHandsTipExistTimer = 0.0f;
    float RandTip1ExistTimer = 0.0f;
    float RandTip2ExistTimer = 0.0f;


    float timer = 0.0f;
    float ShowRandomTipFrequncy=10.0f;
    bool switchRand = true;

    static bool PlayerCanClapHands = false;
    static bool PlayerCanShakeHands = false;
    static bool ShowFrontHomeCaption = false;
    static bool ShowLeaderSayCaption1 = false;
    static bool ShowFrontSchoolCaption = false;
    static bool ShowLeaderSayCaption2 = false;
    #endregion

    void Start()
    {
        gameControllerState = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameControllerState>();
    }

    void Update()
    {

        #region 玩家进入和退出的提示信息
        if (!gameControllerState.getAlreadyHasPlayer())
        {
            if (GUItest.playerEnterIn)
            {
                Show(UIRoot.transform.FindChild(TipNameHash.PlayerEnterTip).gameObject);
                hasPlayerEnterTip = true;
                Hide(UIRoot.transform.FindChild(TipNameHash.PlayerExitTip).gameObject);
                hasPlayerExitTip = false;
                Hide(UIRoot.transform.FindChild(TipNameHash.PlayerEnterWrongTip).gameObject);
                hasPlayerEnterWrong = false;
            }
        }
        else
        {
            if (GUItest.playerEnterWrong)
            {
                Show(UIRoot.transform.FindChild(TipNameHash.PlayerEnterWrongTip).gameObject);
                hasPlayerEnterWrong = true;
                Hide(UIRoot.transform.FindChild(TipNameHash.PlayerExitTip).gameObject);
                hasPlayerExitTip = false;
                Hide(UIRoot.transform.FindChild(TipNameHash.PlayerEnterTip).gameObject);
                hasPlayerEnterTip = false;
            }
            if (GUItest.playerExit)
            {
                Show(UIRoot.transform.FindChild(TipNameHash.PlayerExitTip).gameObject);
                hasPlayerExitTip = true;
                Hide(UIRoot.transform.FindChild(TipNameHash.PlayerEnterTip).gameObject);
                hasPlayerEnterTip = false;
                Hide(UIRoot.transform.FindChild(TipNameHash.PlayerEnterWrongTip).gameObject);
                hasPlayerEnterWrong = false;
            }
            if (hasPlayerEnterTip)
            {
                PlayerEnterTipExistTimer += Time.deltaTime;
                if (PlayerEnterTipExistTimer > ShowTipTime)
                {
                    Hide(UIRoot.transform.FindChild(TipNameHash.PlayerEnterTip).gameObject);
                    hasPlayerEnterTip = false;
                    PlayerEnterTipExistTimer = 0.0f;
                }
            }
            else
            { PlayerEnterTipExistTimer = 0.0f; }

            if (hasPlayerEnterWrong)
            {
                PlayerEnterWrongTipExistTimer += Time.deltaTime;
                if (PlayerEnterWrongTipExistTimer > ShowTipTime)
                {
                    Hide(UIRoot.transform.FindChild(TipNameHash.PlayerEnterWrongTip).gameObject);
                    hasPlayerEnterWrong = false;
                    PlayerEnterWrongTipExistTimer = 0.0f;
                }
            }
            else
            { PlayerEnterWrongTipExistTimer = 0.0f; }


            if (hasPlayerExitTip)
            {
                PlayerExitTipExistTimer += Time.deltaTime;
                if (PlayerExitTipExistTimer > ShowTipTime)
                {
                    Hide(UIRoot.transform.FindChild(TipNameHash.PlayerExitTip).gameObject);
                    hasPlayerExitTip = false;
                    PlayerExitTipExistTimer = 0.0f;
                }
            }
            else
            { PlayerExitTipExistTimer = 0.0f; }
        }
        #endregion

        #region 玩家可以握手的提示信息
        //如果有多个动画效果叠加怎么处理：使用TweenGroup，把固定的动画的组别设置为1，出现的动画的组别设置为0
        if (PlayerCanShakeHands)
        {
            Show(UIRoot.transform.FindChild(TipNameHash.CanShakeHandsTip).gameObject);
            hasCanShakeHandsTip = true;
        }
        else
        {
            Hide(UIRoot.transform.FindChild(TipNameHash.CanShakeHandsTip).gameObject);
            hasCanShakeHandsTip = false;
        }
        if (hasCanShakeHandsTip)
        {
            CanShakeHandsTipExistTimer += Time.deltaTime;
            if (PlayerExitTipExistTimer > ShowTipTime)
            {
                Hide(UIRoot.transform.FindChild(TipNameHash.CanShakeHandsTip).gameObject);
                hasCanShakeHandsTip = false;
                CanShakeHandsTipExistTimer = 0.0f;
            }
        }
        else { CanShakeHandsTipExistTimer = 0.0f; }
        #endregion

        #region 显示随机提示消息
        //如果当前游戏存在一个玩家并且玩家还不能和主席握手，并且玩家在Idle或者走路的状态就开启随机提示
        if (gameControllerState.getAlreadyHasPlayer() && !hasCanShakeHandsTip
             && (GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().GetFSM().CurrentState() == GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().States.IdleState
             || GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().GetFSM().CurrentState() == GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().States.WalkingState)
             && GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>().GetInteractiveSwitch())
        { switchRand = true; }
        else { switchRand = false; }

        //随机地显示一些提示信息
        if (switchRand)
        {
            timer += Time.deltaTime;
            if (timer > ShowRandomTipFrequncy)
            {
                if (Random.Range(0, 10) < Const.SHOW_RANDTIP_POSSIBILITY * 10)
                {
                    float rand = Random.Range(0, 10);
                    if (rand < 5)
                    {
                        Show(UIRoot.transform.FindChild(TipNameHash.RandTips01).gameObject);
                        hasRandTip1 = true;
                    }
                    else
                    {
                        Show(UIRoot.transform.FindChild(TipNameHash.RandTips02).gameObject);
                        hasRandTip2 = true;
                    }
                }
                timer = 0.0f;
            }
        }
        #endregion

        #region 控制随机消息的显示时间
        if (hasRandTip1)
        {
            RandTip1ExistTimer += Time.deltaTime;
            if (RandTip1ExistTimer > ShowTipTime)
            {
                Hide(UIRoot.transform.FindChild(TipNameHash.RandTips01).gameObject);
                hasRandTip1 = false;
                RandTip1ExistTimer = 0.0f;
            }
        }
        else { RandTip1ExistTimer = 0.0f; }

        if (hasRandTip2)
        {
            RandTip2ExistTimer += Time.deltaTime;
            if (RandTip2ExistTimer > ShowTipTime)
            {
                Hide(UIRoot.transform.FindChild(TipNameHash.RandTips02).gameObject);
                hasRandTip2 = false;
                RandTip2ExistTimer = 0.0f;
            }
        }
        else { RandTip2ExistTimer = 0.0f; }
        #endregion

        #region 字幕的显示
        if (ShowFrontHomeCaption)
        {
            UIRoot.transform.FindChild(TipNameHash.FrontHome_Caption).gameObject.SetActive(true);
            ShowFrontHomeCaption = false;
        }

        if (ShowLeaderSayCaption1)
        {
            UIRoot.transform.FindChild(TipNameHash.LeaderSpeech1_Caption).gameObject.SetActive(true);
            ShowLeaderSayCaption1 = false;
        }

        if (ShowFrontSchoolCaption)
        {
            UIRoot.transform.FindChild(TipNameHash.FrontSchool_Caption).gameObject.SetActive(true);
            ShowFrontSchoolCaption = false;
        }

        if (ShowLeaderSayCaption2)
        {
            UIRoot.transform.FindChild(TipNameHash.LeaderSpeech2_Caption).gameObject.SetActive(true);
            ShowLeaderSayCaption2 = false;
        }


        #endregion


        #region 玩家可以鼓掌的提示信息
        //如果有多个动画效果叠加怎么处理：使用TweenGroup，把固定的动画的组别设置为1，出现的动画的组别设置为0
        if (PlayerCanClapHands)
        {
            Show(UIRoot.transform.FindChild(TipNameHash.CanClapHandsTip).gameObject);
            hasCanClapHandsTip = true;
        }
        else
        {
            Hide(UIRoot.transform.FindChild(TipNameHash.CanClapHandsTip).gameObject);
            hasCanClapHandsTip = false;
        }
        if (hasCanClapHandsTip)
        {
            CanClapHandsTipExistTimer += Time.deltaTime;
            if (CanClapHandsTipExistTimer > ShowTipTime)
            {
                Hide(UIRoot.transform.FindChild(TipNameHash.CanClapHandsTip).gameObject);
                hasCanClapHandsTip = false;
                CanClapHandsTipExistTimer = 0.0f;
            }
        }
        else { CanClapHandsTipExistTimer = 0.0f; }
        #endregion

    }

    #region 内部服务函数
   
    void Show(GameObject tip) 
    {
        UITweener[] tweens = tip.GetComponents<UITweener>();
        for (int i = 0; i < tweens.Length; i++)
        {
            //出现的动画只有一个，摇摆的动画不算在内,把摇摆动画的组别设置为1
            if (tweens[i].tweenGroup != 1)
            {
                //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                tweens[i].PlayForward();
            }
        }
        if (tip.GetComponent<AudioSource>())
        {
            tip.GetComponent<AudioSource>().enabled = true;
        }
    }

    void Hide(GameObject tip)
    {
        UITweener[] tweens = tip.GetComponents<UITweener>();
        for (int i = 0; i < tweens.Length; i++)
        {
            if (tweens[i].tweenGroup != 1)
            {
                tweens[i].PlayReverse();
            }
        }
        if (tip.GetComponent<AudioSource>())
        {
            tip.GetComponent<AudioSource>().enabled = false;
        }
    }
    #endregion
    #region 外部接口

    public static void SetPlayerCanShakeHands(bool flag)
    {
        PlayerCanShakeHands = flag; 
    }
    public static void SetPlayerCanClapHands(bool flag)
    {
        PlayerCanClapHands = flag;
    }

    public static void SetShowFrontHomeCaption(bool flag) { ShowFrontHomeCaption = flag; }
    public static void SetShowLeaderSayCaption1(bool flag) { ShowLeaderSayCaption1 = flag; }

    public static void SetShowFrontSchoolCaption(bool flag) { ShowFrontSchoolCaption = flag; }
    public static void SetShowLeaderSayCaption2(bool flag) { ShowLeaderSayCaption2 = flag; }

    public void WhenTipWithAudioFinished(GameObject tip)
    {
        tip.GetComponent<AudioSource>().Stop();
    }
    #endregion

}
