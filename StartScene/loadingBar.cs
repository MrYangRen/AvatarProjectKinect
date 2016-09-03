using UnityEngine;
using System.Collections;

public class loadingBar : MonoBehaviour {

    public static loadingBar _instance;
    UISlider slider;
    float timer = 0.0f;//计时器
    public float start = 4.0f;//经过4s认为bar满格
    public GameObject blackBG;

    void Awake()
    {
        _instance = this;
        slider = this.GetComponent<UISlider>();
        this.gameObject.SetActive(false);
        slider.value = 0.0f;
        blackBG.SetActive(false);
    }

	void Update () 
    {
        timer += Time.deltaTime;
        slider.value = timer / start;
        if (slider.value >= 1F)
        {
            //淡入黑色幕布
            blackBG.SetActive(true);

        }
    }
    public void ReSet()
    {
        timer = 0.0f;
        slider.value = 0.0f;
    }

} 
