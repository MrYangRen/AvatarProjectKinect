using UnityEngine;
using System.Collections;

public class DestroyNPC : MonoBehaviour
{

    public GameObject[] NPCs;
    private Transform leader;

    // Use this for initialization
    void Start()
    {
        leader = GameObject.FindGameObjectWithTag(Tags.Leader).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(leader.position, this.transform.position) < 4)
        {
            //  Debug.Log(Vector3.Distance(leader.position, this.transform.position));
            if (this.gameObject.name == "35")
            {
                if (NPCs[0].activeSelf == false)
                {
                    //Debug.Log("35 ok");
                    for (int i = 0; i < NPCs.Length; i++)
                    {
                        NPCs[i].SetActive(false);
                    }
                    NPCs[0].SetActive(true);
                    //NPCs[2].SetActiveRecursively(true);
                }
            }

            else if (this.gameObject.name == "01")
            {
                if (NPCs[1].activeSelf == false)
                {
                    //Debug.Log("01 ok");
                    for (int i = 0; i < NPCs.Length; i++)
                    {
                        NPCs[i].SetActive(false);
                    }
                    NPCs[1].SetActive(true);
                    //NPCs[2].SetActiveRecursively(true);
                }
            }
            else if (this.gameObject.name == "15")
            {
                if (NPCs[2].activeSelf == false)
                {
                    //Debug.Log("15 ok");
                    for (int i = 0; i < NPCs.Length; i++)
                    {
                        NPCs[i].SetActive(false);
                    }
                    NPCs[2].SetActive(true);
                    // NPCs[2].SetActiveRecursively(true);
                }
            }
            //街道那里一起设置
            else if (this.gameObject.name == "40")
            {
                if (NPCs[3].activeSelf == false)
                {
                    //Debug.Log("40 ok");
                    for (int i = 0; i < NPCs.Length; i++)
                    {
                        NPCs[i].SetActive(false);
                    }
                    NPCs[3].SetActive(true);
                    NPCs[4].SetActive(true);
                    NPCs[5].SetActive(true);
                    // NPCs[2].SetActiveRecursively(true);
                }
            }

            //else if (this.gameObject.name == "45")
            //{
            //    if (NPCs[3].activeSelf == false)
            //    {
            //        //Debug.Log("40 ok");
            //        for (int i = 0; i < NPCs.Length; i++)
            //        {
            //            NPCs[i].SetActive(false);
            //        }
            //        NPCs[3].SetActive(true);
            //        NPCs[4].SetActive(true);
            //        NPCs[5].SetActive(true);
            //        // NPCs[2].SetActiveRecursively(true);
            //    }
            //}


            else if (this.gameObject.name == "51")
            {
                if (NPCs[6].activeSelf == false)
                {
                    //Debug.Log("51 ok");
                    for (int i = 0; i < NPCs.Length; i++)
                    {
                        NPCs[i].SetActive(false);
                    }
                    NPCs[6].SetActive(true);
                    // NPCs[2].SetActiveRecursively(true);
                }
            }

            else if (this.gameObject.name == "19")
            {
                if (NPCs[7].activeSelf == false)
                {
                    //Debug.Log("19 ok");
                    for (int i = 0; i < NPCs.Length; i++)
                    {
                        NPCs[i].SetActive(false);
                    }
                    NPCs[7].SetActive(true);
                    //NPCs[2].SetActiveRecursively(true);
                }
            }



        }
    }
}
