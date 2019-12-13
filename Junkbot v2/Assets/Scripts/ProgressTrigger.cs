using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTrigger : MonoBehaviour
{
    [SerializeField]
    private QuestGiver qg;

    public int toQuest;
    public int toLevel;
    public int toSection;

    private void Start()
    {
        qg = FindObjectOfType<QuestGiver>();
    }

    private void Update()
    {
        if (qg.questCount >= toQuest)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered Progress Trigger");

            qg.waitingForProgressTrigger = false;
            GameManager2.Instance.toCheckpoint(toLevel, toSection);
            qg.toQuest(toQuest);

        }
    }
}
