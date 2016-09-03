using UnityEngine;
using System.Collections;

public class MemoryCameras : MonoBehaviour {


    private float timer;
    private Camera MainCamera;

    void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag(Tags.Camera).GetComponent<Camera>();
        MainCamera.enabled = false;
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 28)
        {
            this.gameObject.SetActive(false);
            MainCamera.enabled = true;

        }
    }
 
}
