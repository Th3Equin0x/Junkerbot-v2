using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBuilder : MonoBehaviour
{
    public GameObject prebuiltobject;   //object that is going to be placed on the housespawner 
    public GameObject builtobject;
    public AudioClip housebuilderfx;

    [SerializeField]
    private QuestGiver qg;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == prebuiltobject)
        {
            if (builtobject.name == "Roof")
            {
                qg.waitingForRoofTrigger = false;
                qg.toQuest(11);
            }
            if (builtobject.name == "Door")
            {
                qg.waitingForDoorTrigger = false;
                qg.toQuest(18);
            }


            Debug.Log("plank collided with roof");

            prebuiltobject.SetActive(false);
            builtobject.SetActive(true);
            AudioManager.Instance.PlaySFX(housebuilderfx, 1.5f);    //add sound fx 

            GameManager2.Instance.NextCheckpoint();
        }
    }
}
