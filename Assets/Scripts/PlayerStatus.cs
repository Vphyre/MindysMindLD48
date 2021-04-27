using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    public static int mindPoints = 0;
    public static int hpCurrent = 0;
    public static int hpDefault = 3;
    public Text mindPointsText;    
    public GameObject [] healthImgs;
    public static int mpPhase1=0;
    public static int mpPhase2=0;
    public static int mpPhase3=0;
    public static string previousScene;
    void Start()
    {
        hpCurrent = hpDefault;
       
       
    }

    void Update()
    {
        MindPointsController();
        HealthController();
        MPStats();
    }

    void MindPointsController()
    {
        mindPointsText.text = mindPoints.ToString();
    }

    void HealthController()
    {
        if(hpCurrent==3)
        {
            healthImgs[0].SetActive(true);
            healthImgs[1].SetActive(true);
            healthImgs[2].SetActive(true);
        }
        else if (hpCurrent==2)
        {
            healthImgs[0].SetActive(false);
            healthImgs[1].SetActive(true);
            healthImgs[2].SetActive(true);
        }
        else if(hpCurrent==1)
        {
            healthImgs[0].SetActive(false);
            healthImgs[1].SetActive(false);
            healthImgs[2].SetActive(true);
        }
        else
        {
            healthImgs[0].SetActive(false);
            healthImgs[1].SetActive(false);
            healthImgs[2].SetActive(false);
            //GameOver
        }
    }
    private void MPStats()
    {
        if(previousScene=="Phase1")
        {
            mpPhase1 = 0;
        }
        if(previousScene=="Phase2" && mpPhase2==0)
        {
            mpPhase2 = PlayerStatus.mindPoints;
        }
        if(previousScene=="Phase3" && mpPhase3==0)
        {
            mpPhase3 = PlayerStatus.mindPoints;
        }

    }
}
