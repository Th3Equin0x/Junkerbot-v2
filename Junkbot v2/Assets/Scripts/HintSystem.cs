using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    
    public GameObject HintsystemCanvas;
    public GameObject Imagehelper;
    public AudioClip music1;
    public AudioClip music2; //er music 


    public void SetMusicVolume(float volume)
    {
        volume = 0.0001f;
    }


    void Start()//play music 
    {
        Debug.Log("1. music1 is playing");
        AudioManager.Instance.PlayMusicWithCrossFade(music1, .01f);

    }
    private bool helpActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("press down on Q");
           
            if (helpActive == true)
            {
                helpActive = false;
                Imagehelper.SetActive(false);
                Debug.Log(" hint goes away");
                AudioManager.Instance.PlayMusicWithCrossFade(music1, .01f);


            }
            else
            {
                Debug.Log("hint system pops up");
                helpActive = true;
                AudioManager.Instance.PlayMusicWithCrossFade(music2, .01f);
                Imagehelper.SetActive(true);
                
                


            }
            

        }
       


    }
}
