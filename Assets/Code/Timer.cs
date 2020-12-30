using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //opetion 1
    float currenttime = 0f;
    [SerializeField] public static float statringtime = 60f;

    [SerializeField] Text countDowntext;
    // Start is called before the first frame update
    void Start()
    {
        currenttime = statringtime;
    }

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        currenttime -= 1 * Time.deltaTime;
        if (currenttime>59)
        {
            current = currenttime - 60;
            if (currenttime<=69 && currenttime>60)
            {
                countDowntext.text = "01:0" + current.ToString("0");
            }
            else
            {
                countDowntext.text = "01:" + current.ToString("0");
            }
            
        }

        if (currenttime<60)
        {
            countDowntext.text = "00:" + currenttime.ToString("0");
        }
        
        

        if (currenttime<=0)
        {
            currenttime = 0;
            if (TargetRunner.rodisscrore>TargetRunner.yourscrore)
            {
                countDowntext.text = "Loser \n the roddis get: " + TargetRunner.rodisscrore;
                //TargetRunner.rodisscrore = 0;
                //TargetRunner.yourscrore = 0;
                TargetRunner.availablefood.Clear();
                
            }
            else
            {
                countDowntext.text = "Winner! \n the roddit got: " + TargetRunner.rodisscrore;
                //TargetRunner.rodisscrore = 0;
                //TargetRunner.yourscrore = 0;
                TargetRunner.availablefood.Clear();
            }

        }
    }
}
