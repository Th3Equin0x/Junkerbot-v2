using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawConnectedAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HingeJoint hj = this.GetComponent<HingeJoint>();
        hj.connectedAnchor = new Vector3(0, -0.05f, 0);
    }
}
