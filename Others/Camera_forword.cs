using UnityEngine;
using System.Collections;

public class Camera_forword: MonoBehaviour
{

    public float endZ = 1500.0f;
    public float speed = 6.0f;
    private bool forwarding;
    public Transform center;
    private float timer;

    void Awake()
    {
        forwarding = true;
    }

    void Update()
    {
        
        if (forwarding)
        {
            if (transform.position.z> endZ)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.LookAt(center);
            }
            else
            {
                forwarding = false;
            }
        }
        else
        {
            this.enabled = false;
        }
    }

}
