using UnityEngine;
using System.Collections;

public class GameControllerOnStartScene : MonoBehaviour {

    public GameObject Start_Root;
    public GameObject IntroRoot;
    public int NextScene = 1;

    public void OnBlackFadeInFinished()
    {
        Start_Root.SetActive(false);
        IntroRoot.SetActive(true);
        GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<Camera_rotate>().enabled = true;
        GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<Camera_forword>().enabled = true;
    }

    public void LoadNextScene()
    {
        Application.LoadLevel(NextScene);
    }

}
