using UnityEngine;
using System.Linq;
using UnitySteer.Behaviors;
using System.Collections.Generic;
using System.Collections;

public class PathFollowingController : MonoBehaviour
{
    public Transform path;
    private List<Transform> path_list;
    public int path_target_Distance = 5;

    void Start()
    {
        path_list = new List<Transform>();
        int randOfPath = Random.Range(0, 10);

            foreach (Transform k in path)
            {
                if (randOfPath < 7)
                {
                    if (k.gameObject.tag == "RandPath1")
                    {
                        k.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (k.gameObject.tag == "RandPath2")
                    {
                        k.gameObject.SetActive(false);
                    }
                }
           
        }
   
    
        foreach (Transform k in path)
        {
            if (k.gameObject.active)
            {
                path_list.Add(k);
                path_list.OrderBy(t => t.gameObject.name);
            }
        }


    }

    void Update()
    {
        GetTargetPoint();
    }

    void GetTargetPoint()
    {
        if (path_list.Count != 0)
        {
            if (Vector3.Distance(path_list[0].position, transform.position) > path_target_Distance)
            {
                gameObject.GetComponent<SteerForPoint>().enabled = true;
                gameObject.GetComponent<SteerForPoint>().TargetPoint = path_list[0].position;
            }
            else
            {
                path_list[0].gameObject.SetActive(false);
                path_list.Remove(path_list[0]);
            }
        }
    }
}
