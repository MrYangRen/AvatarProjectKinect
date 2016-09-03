using UnityEngine;
using System.Collections;

public class HumanEntity : BaseGameEntity
{
    [HideInInspector]
    //交互对象
    public GameObject target = null;
    float waittime;
    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    #region Time
    public float GetWaittime()
    {
        return waittime;
    }

    public void PlusWaittime()
    {
        waittime += Time.deltaTime;
    }

    public void CleanWaittime()
    {
        waittime = 0;
    }
    #endregion

    public void LookAt(Transform focusPoint)
    {
        transform.LookAt(focusPoint);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }


    public Component GetTargetComponet(GameObject temporary)
    {
        Component target_Component = null;

        if (temporary.GetComponent<Follower>())
            target_Component = temporary.GetComponent<Follower>();
        else if (temporary.GetComponent<Leader>())
            target_Component = temporary.GetComponent<Leader>();
        else if (temporary.GetComponent<People>())
            target_Component = temporary.GetComponent<People>();
        else if (target.GetComponent<Player>())
            target_Component = target.GetComponent<Player>();

        return target_Component;
    }


}
