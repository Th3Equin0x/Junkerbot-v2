using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MoveableObject>())
        {
            other.GetComponent<MoveableObject>().ObjectRespawn();
            if (other.GetComponent<HingeJoint>())
            {
                other.GetComponent<HingeJoint>().connectedBody.gameObject.transform.position = other.transform.position + new Vector3(0, 1, 0);
                other.GetComponent<HingeJoint>().connectedBody.gameObject.transform.eulerAngles = other.transform.eulerAngles;
                other.GetComponent<HingeJoint>().connectedBody.velocity = Vector3.zero;
            }
        }
        if (other.tag == "Player")
        {
            GameManager2.Instance.PlayerReset();
        }
    }
}
