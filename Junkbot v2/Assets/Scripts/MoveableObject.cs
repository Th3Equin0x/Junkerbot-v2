using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Vector3 HJRelativeRespawnPos;
    private Vector3 HJRelativeRespawnRot;

    [SerializeField]
    private Vector3 resetPos;

    [SerializeField]
    private Vector3 resetRot;

    private void Start()
    {
        this.tag = "moveable";
        resetPos = this.transform.position;
        resetRot = this.transform.eulerAngles;

        if (GetComponent<HingeJoint>())
        {
            HJRelativeRespawnPos = this.transform.position - this.gameObject.GetComponent<HingeJoint>().connectedBody.transform.position;
            HJRelativeRespawnPos = this.transform.eulerAngles - this.gameObject.GetComponent<HingeJoint>().connectedBody.transform.eulerAngles;
        }
    }

    public void ObjectRespawn()
    {
        this.gameObject.layer = 9;                              //changes object back to "object" in case it's still "selected".
        this.GetComponent<Rigidbody>().velocity = Vector3.zero; //removes velocity/force from the object
        this.transform.position = resetPos;                   //returns object to its initial position
        this.transform.eulerAngles = resetRot;                   //returns object ro its initial

        if (GetComponent<HingeJoint>())
        {
            this.gameObject.GetComponent<HingeJoint>().connectedBody.transform.position = this.transform.position - HJRelativeRespawnPos;
            this.gameObject.GetComponent<HingeJoint>().connectedBody.transform.eulerAngles = this.transform.eulerAngles - HJRelativeRespawnPos;
        }
    }

    public void SetRespawn(Vector3 pos, Vector3 rot)
    {
        if (this.gameObject.GetComponent<Rigidbody>())
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        resetPos = pos;
        resetRot = rot;
    }

    public Vector3 GetRespawnPos()
    { return resetPos; }

    public Vector3 GetRespawnRot()
    { return resetRot; }

}
