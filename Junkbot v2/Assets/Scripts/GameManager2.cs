using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    private static GameManager2 _instance;
    public static GameManager2 Instance { get { return _instance; } }

    [System.Serializable]
    public struct Level
    {
        public Vector3 initPos;                 //the place the player will be spawned at level start 
        public Vector3 initRot;                 //the rotation at which the player will be spawned at level start

        public MoveableObject[] moveableobjects;        //moveable objects that will need to be respawen if the player resets

        [SerializeField]
        public List<Checkpoint> sections;       //List of checkpoints for this level

        private bool _completed;
        public bool completed
        {
            get { return _completed; }
            set { _completed = value; }
        }

        public Level(Checkpoint cp)
        {
            initPos = cp.respawnPos;
            initRot = cp.respawnRot;

            moveableobjects = FindObjectsOfType<MoveableObject>();

            sections = new List<Checkpoint>();
            sections.Add(cp);

            _completed = false;
        }
    }

    [System.Serializable]
    public struct Checkpoint
    {
        public Vector3 respawnPos;              //where the player will be placed if they reset during this checkpoint
        public Vector3 respawnRot;              //respawn rotation

        public Checkpoint(Vector3 rPos, Vector3 rRot)
        {
            respawnPos = rPos;
            respawnRot = rRot;
        }
    }

    [SerializeField]
    public Level[] levels = new Level[5];

    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int currentSection;

    private FirstPersonAIO player;
    private Transform HeadJoint;
    public ObjectManipulation playerObjManip;

    [SerializeField]
    private GameObject RoofMoveable;
    [SerializeField]
    private GameObject DoorMoveable;

    public GameObject L1B;
    public GameObject L2B;
   
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        player = FindObjectOfType<FirstPersonAIO>();
        playerObjManip = player.GetComponent<ObjectManipulation>();
        HeadJoint = GameObject.Find("HeadJoint").transform;

        currentLevel = 0;
        currentSection = 0;

        player.transform.position = levels[0].initPos;
        player.transform.eulerAngles = levels[0].initRot;
        HeadJoint.eulerAngles = levels[0].initRot;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerReset();
    }

    public void EndLevel()
    {
        Debug.Log("End Level Called");

        if (currentLevel == 1)
        {
            RoofMoveable.SetActive(true);
            L1B.SetActive(true);
            L2B.SetActive(false);
        }
        else if (currentLevel == 2)
        {
            DoorMoveable.SetActive(true);
            L2B.SetActive(true);
        }

        if (currentLevel != 0)
        {
            levels[currentLevel].completed = true;

            currentLevel = 0;
            player.transform.position = levels[0].initPos;
            player.transform.eulerAngles = levels[0].initRot;
            HeadJoint.eulerAngles = levels[0].initRot;
        }
        else if (levels[1].completed != true)
        {
            currentLevel = 1;
        }
        else //if (levels[1].completed == true
        {
            currentLevel = 2;
        }

        currentSection = 0;
    }

    public void NextCheckpoint()
    {
        Debug.Log("Next Checkpoint Log");

        if (currentSection + 1 >= levels[currentLevel].sections.Count)
        {
            EndLevel();
        }
        else
        {
            //iterate through Levels[current].sections[]
            currentSection++;

            //update object respawn positions to match where they were when the checkpoint iterated???
            foreach (MoveableObject O in levels[currentLevel].moveableobjects)
            {
                if (O.gameObject.activeInHierarchy == true)
                {
                    O.SetRespawn(O.transform.position, O.transform.eulerAngles);
                }
            }
        }
    }

    public void toCheckpoint(int l, int s)
    {
        Debug.Log("toCheckpoint Log");

        if (currentSection + 1 >= levels[currentLevel].sections.Count && s == 0)
        {
            EndLevel();
        }
        else
        {
            currentLevel = l;
            currentSection = s;

            foreach (MoveableObject O in levels[currentLevel].moveableobjects)
            {
                if (O.gameObject.activeInHierarchy == true)
                {
                    O.SetRespawn(O.transform.position, O.transform.eulerAngles);
                }
            }
        }
    }

    public void PlayerReset()
    {
        Debug.Log("Player Reset Called");

        //respawn player
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = levels[currentLevel].sections[currentSection].respawnPos;
        player.transform.eulerAngles = levels[currentLevel].sections[currentSection].respawnRot;
        HeadJoint.eulerAngles = player.transform.eulerAngles;

        //respawn objects
        foreach (MoveableObject O in levels[currentLevel].moveableobjects)
        {
            O.ObjectRespawn();
        }

        if (playerObjManip.selected)
        {
            playerObjManip.DropSelected();
        }
    }
}
