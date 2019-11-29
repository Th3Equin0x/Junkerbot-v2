using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentKeepChildren : MonoBehaviour
{
    Transform c;

    // Start is called before the first frame update
    void Awake()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            // objectA is not the attached GameObject, so you can do all your checks with it.
            var objectA = transform.GetChild(i);
            objectA.transform.parent = null;
            // Optionally destroy the objectA if not longer needed
        }

        Destroy(this.gameObject);
    }
}
