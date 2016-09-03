using UnityEngine;
using UnitySteer.Behaviors;
using System.Collections;

public class SetPeopleOffset : MonoBehaviour {
    private float offsetX;
    private float offsetY;
    private float plusX = -0.6f;
    private float plusY = -0.6f;


    void Start() {
        offsetX = 2;
        offsetY = 4;
        foreach (Transform child in this.transform) {
            //SteerForLeadFollowing follow = child.gameObject.GetComponent<SteerForLeadFollowing>();
            child.gameObject.GetComponent<SteerForLeadFollowing>().Offset.Set(offsetX, 0, offsetY);
            //follow.Offset.Set(offsetX,0,offsetY);
            offsetX += plusX;
            offsetY += plusY;
            if (offsetX <= -2) { plusX = 0.6f; }
            if (offsetY >= 0.5) { plusY = 0.6f; }
        }
    }


}
