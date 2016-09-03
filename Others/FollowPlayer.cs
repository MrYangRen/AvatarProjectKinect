using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    private bool isRotating = false;


    public Vector3 offset;//位置偏移
    public float distance = 0;//视野远近
    public float scrollSpeed = 8f;
    public float dis_min = 2f;//控制视野不无限地拉近和拉远
    public float dis_max = 10f;
    public float rotate_x_min = 10f;//围绕x轴最大能旋转的角度
    public float rotate_x_max = 80f;
    public float rotateSpeed = 3f;

	// Use this for initialization
	void Start () {
        transform.LookAt(target.position);//在一开始朝向玩家
        offset = transform.position - target.position;
        //一直保持一开始的偏移量

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + offset;
        //一个改变大小，一个改变方向（整体）。注意调用顺序
        RotateView();
        ScrollView();
	}


    //处理视野拉近拉远效果
    void ScrollView()
    {
        //使用鼠标中键滑动来控制.不滑动返回0，向上滑返回正，向下滑返回负
        //主观感受：向后滑动为拉远，向前滑动拉近
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel")*scrollSpeed;
        distance = Mathf.Clamp(distance, dis_min, dis_max);
        offset = offset.normalized * distance;

    }

    //处理鼠标右键按下左右移动的时候视野的旋转
    void RotateView()
    {
        //Input.GetAxis("Mouse X");//得到鼠标在水平方向的滑动
        //Input.GetAxis("Mouse Y");//得到鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            //一个绕着玩家的轴，一个绕着自身轴。绕着两个不停的轴。防止旋转同时作用，
            transform.RotateAround(target.position, target.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(target.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            //这个旋转需要进行限制，因为可能会旋转过头而看不到玩家
            //而且这个旋转影响了position和rotation.一旦旋转过头这两个属性要恢复到原来的值
            float x = transform.eulerAngles.x;
            if (x < rotate_x_min || x > rotate_x_max)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;

            }

        }
        //注意旋转之后要改变offset向量，否则会一直绕着自身旋转，因为这个向量被重置回来了
        offset = transform.position - target.position;
        //更新offset


    }
}
