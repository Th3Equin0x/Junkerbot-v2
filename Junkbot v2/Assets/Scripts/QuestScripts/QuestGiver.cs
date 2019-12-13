using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class QuestGiver : MonoBehaviour
{
    public AudioClip Questcompletetsfx;

    public AudioClip music1;
    public Quest[] quests;
    public Quest activeQuest;
    public int questCount;

    public GameObject player;

    public GameObject questWindow;
    private GameObject questMarker;
    [SerializeField]
    private GameObject ImageHelper;
    public GameObject HintSystem;

    static Text OBJECTIVE;
    public Text titletext;
    public Text descriptiontext;

    public bool waitingForProgressTrigger;
    public bool waitingForRoofTrigger;
    public bool waitingForDoorTrigger;

    private void Start()
    {
        questMarker = GameObject.Find("Quest Marker");

        questCount = 0;
        waitingForProgressTrigger = false;

        OpenQuestWindow();
    }

    private void Update()
    {
        if (questCount == 0 && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {   //mouse looking
            nextQuest();
        }

        if (questCount == 1 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {   //wasd movement
            nextQuest();
            questMarker.SetActive(true);
            //GameManager2.Instance.EndLevel();
        }

        if (questCount == 3 && Input.GetKeyDown(KeyCode.Space))
        {   //jumping
            nextQuest();
        }

        if (questCount == 4 && player.GetComponent<ObjectManipulation>().selected)
        {   //picking up
            waitingForProgressTrigger = true;
            nextQuest();
        }

        if (questCount == 6 && (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {   //rotating
            waitingForProgressTrigger = true;
            nextQuest();
        }

        if (questCount == 8 && GameObject.Find("Barrier").GetComponent<Rigidbody>().velocity != Vector3.zero)
        {   //combining
            waitingForProgressTrigger = true;
            nextQuest();
        }

        if (questCount == 17 && !waitingForDoorTrigger)
        {   //building
            nextQuest();
        }

        //final quest
    }

    public void OpenQuestWindow()
    { 
        questWindow.SetActive(true);
        activeQuest = quests[questCount];
        titletext.text = activeQuest.title;
        descriptiontext.text = activeQuest.description;
        questMarker.transform.position = activeQuest.MarkerPos;

        if (quests[questCount].hintImage)
        {
            HintSystem.SetActive(true);
            ImageHelper.GetComponent<Image>().sprite = activeQuest.hintImage;
        }
        else
            HintSystem.SetActive(false);
    }

    public void nextQuest()
    {
        questCount++;
        ImageHelper.SetActive(false);
        AudioManager.Instance.PlayMusicWithCrossFade(music1, .01f);
        AudioManager.Instance.PlaySFX(Questcompletetsfx, 100.0f);    //add sound fx 
        OpenQuestWindow();
    }

    public void toQuest(int q)
    {
        questCount = q;
        ImageHelper.SetActive(false);

        AudioManager.Instance.PlaySFX(Questcompletetsfx, 100.0f);    //add sound fx 
        OpenQuestWindow();
    }
}
