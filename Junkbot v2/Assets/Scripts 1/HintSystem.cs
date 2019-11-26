using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    public AudioClip hintsystemsfx;
    public GameObject HintsystemCanvas;
    public GameObject Imagehelper;

    private bool helpActive = false;

    void Update()
    {
        HintsystemCanvas.SetActive(true);
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            
            if (HintsystemCanvas == true)
            {
                print("Q was pressed");
                AudioManager.Instance.PlaySFX(hintsystemsfx, 1.0f);
                Imagehelper.SetActive(true);
                
            }
          if(Imagehelper ==true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Imagehelper.SetActive(false);
                }
                    
            }
       
            
        }*/

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (helpActive == true)
            {
                helpActive = false;
                Imagehelper.SetActive(false);
            }
            else
            {
                helpActive = true;
                Imagehelper.SetActive(true);
                AudioManager.Instance.PlaySFX(hintsystemsfx, 1.0f);
            }
        }
    }
}
