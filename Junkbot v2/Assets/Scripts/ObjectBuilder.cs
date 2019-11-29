using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuilder : MonoBehaviour
{
    public GameObject otherObject;
    public GameObject resultObject;
    public AudioClip ObjectBuiltsfx;

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject == otherObject)
        {
            AudioManager.Instance.PlaySFX(ObjectBuiltsfx, 1.0f);    //add sound fx 

            Debug.Log("ObjectBuilder activated");
            Vector3 origObjPos = this.transform.position;
            Quaternion origObjRot = this.transform.rotation;

            resultObject.SetActive(true);
            resultObject.transform.position = origObjPos;
            resultObject.transform.rotation = origObjRot;
            

            Destroy(otherObject);
            Destroy(this.gameObject);
        }
    }


}
