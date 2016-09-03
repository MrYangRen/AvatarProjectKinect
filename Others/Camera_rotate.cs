using UnityEngine;
using System.Collections;


//移动过后 摄像机才开始绕着center进行旋转

public class Camera_rotate : MonoBehaviour {

    public Transform center;
    public float RotateSpeed = 0.5f;
    public float RotateTime = 7.0f;    //旋转的时间
    public float Raidus = 500.0f;

    private bool isRotating=false;//
    private float w=0.0f;//角速度，每秒转过多少度
    private float timer=0.0f;
    private Camera_forword cameraForword;

    void Awake()
    {
        isRotating = true;
        cameraForword = this.GetComponent<Camera_forword>();
        //半径保持不变
    }

    void Update()
    {
        if (!cameraForword.enabled)
        {
            if (isRotating)
            {
                if (timer < RotateTime)
                {
                    timer += Time.deltaTime;
                    w += RotateSpeed * Time.deltaTime;
                    if(w>360)
                    { w = 0; }
                    float x = center.position.x+Mathf.Cos(w) * Raidus;
                    float z = center.position.z+Mathf.Sin(w) * Raidus;
                    //在水平平面上（xz平面上）做圆周运动
                    transform.position = new Vector3(x, transform.position.y, z);
                    transform.LookAt(center);
                }
                else
                {
                    isRotating = false;
                }
            }
            else
            {
                this.enabled = false;
            }
        }
    }

}
