using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuilder : MonoBehaviour
{
    public GameObject otherObject;
    public GameObject resultObject;
    public AudioClip ObjectBuiltsfx;

    public Vector3 prebuiltToPos;
    public Vector3 prebuiltToRot;

    public Vector3 otherToPos;
    public Vector3 otherToRot;

    public Vector3 resultRespawnPos;
    public Vector3 resultRespawnRot;

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject == otherObject)
        {
            AudioManager.Instance.PlaySFX(ObjectBuiltsfx, 1.0f);    //add sound fx 

            Debug.Log("ObjectBuilder activated");
            Vector3 origObjPos = this.transform.position;
            Quaternion origObjRot = this.transform.rotation;

            this.gameObject.layer = 12;
            collision.gameObject.layer = 12;
            resultObject.layer = 12;

            otherObject.transform.position = otherToPos;
            otherObject.transform.eulerAngles = otherToRot;

            transform.position = prebuiltToPos;
            transform.eulerAngles = prebuiltToPos;

            resultObject.transform.position = origObjPos + new Vector3 (0, 0.1f, 0);
            resultObject.transform.rotation = origObjRot;

            resultObject.SetActive(true);
            resultObject.GetComponent<MoveableObject>().SetRespawn(resultRespawnPos, resultRespawnRot);
            resultObject.layer = 9;

            otherObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }


}
