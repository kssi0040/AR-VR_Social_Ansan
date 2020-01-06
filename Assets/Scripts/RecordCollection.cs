using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordCollection : MonoBehaviour
{
    public GameObject mainCanvas;
    public string strEdu_type = null;
    public Text goldText = null;
    public Text silverText = null;
    public Text blonzText = null;

    private GameObject[] aStampImg = new GameObject[4];
    private int iStampCount = 0;
    private int iAnswerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < aStampImg.Length; ++i)
        {
            aStampImg[i] = mainCanvas.transform.GetChild(i+1).gameObject.transform.GetChild(0).gameObject;
            aStampImg[i].SetActive(false);
        }

        StampCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //================================================= GPS RECORD ============================================//
    void StampCheck()
    {
        string appID = PlayerInfo.Instance.GetAppID();        
        string userID = PlayerInfo.Instance.GetUserID();
        //string kind = "social_sticker_first";
        string kind = "";
        string get_flag = "";
        string edu_type = strEdu_type + "_live";
        string game_type = "AR";

        if (0 == iStampCount)
        {
            kind = strEdu_type + "_sticker_first";
            DatabaseManager.Instance.RecordGetStamp(appID, userID, kind, edu_type);
        }
        else if (1 == iStampCount)
        {
            kind = strEdu_type + "_sticker_second";
            DatabaseManager.Instance.RecordGetStamp(appID, userID, kind, edu_type);
        }
        else if (2 == iStampCount)
        {
            kind = strEdu_type + "_sticker_third";
            DatabaseManager.Instance.RecordGetStamp(appID, userID, kind, edu_type);
        }
        else if (3 == iStampCount)
        {
            kind = strEdu_type + "_sticker_fourth";
            DatabaseManager.Instance.RecordGetStamp(appID, userID, kind, edu_type);
        }
        else if (4 == iStampCount)
        {
            RankInfoView();
            return;
        }
    }

    public void RecordCatchStampInfo(string _strAnswer)
    {
        //Debug.Log("answer: " + _strAnswer);
        string strAnswer = _strAnswer;
        if ("Y" == strAnswer)
        {
            // 이미 획득한 Stamp..            
            aStampImg[iStampCount].SetActive(true);            
        }
        else
        {
            // 획득 안한 Stamp...
            aStampImg[iStampCount].SetActive(false);            
        }
        iStampCount++;
        StampCheck();
    }


    //================================================= RANK RECORD ============================================//
    void RankInfoView()
    {
        // 1, 2, 3 랭크 View...
        string AppID = PlayerInfo.Instance.GetAppID();
        string UserID = PlayerInfo.Instance.GetUserID();
        string BadgeType = "timeAttack";
        string eduType = "social_live";
        
        DatabaseManager.Instance.RecordTimeAtkRank(AppID, UserID, BadgeType, eduType);
    }

    // 받는 부분..
    public void RecordCatchRank(string _strAnswer)
    {
        // 받아서 View....     
        string strAnswer = _strAnswer;

        string strGoldText = "";
        // gold
        int iFirstIDCommaIndex = strAnswer.IndexOf(",");        
        string firstID = strAnswer.Substring(0, iFirstIDCommaIndex);
        Debug.Log("first ID: " + firstID);
        strGoldText +=(firstID + " / ");

        int iFirstSchoolCommaIndex = strAnswer.IndexOf(",", iFirstIDCommaIndex+1);                        
        string firstSchool = strAnswer.Substring(iFirstIDCommaIndex + 1, (iFirstSchoolCommaIndex - iFirstIDCommaIndex)-1);        
        strGoldText += firstSchool;

        int iFirstTimeCommaIndex = strAnswer.IndexOf(",", iFirstSchoolCommaIndex + 1);
        string firstTime = strAnswer.Substring(iFirstSchoolCommaIndex + 1, (iFirstTimeCommaIndex - iFirstSchoolCommaIndex)-1);        
        strGoldText += "\nTime: ";        
        strGoldText += firstTime + " sec";
        goldText.text = strGoldText;

        string strSilverText = "";
        // silver
        int iSecondIDCommaIndex = strAnswer.IndexOf(",", iFirstTimeCommaIndex + 1);
        string scondID = strAnswer.Substring(iFirstTimeCommaIndex + 1, (iSecondIDCommaIndex - iFirstTimeCommaIndex) - 1);        
        strSilverText += (scondID + " / ");

        int iSecondSchoolCommaIndex = strAnswer.IndexOf(",", iSecondIDCommaIndex + 1);
        string secondSchool = strAnswer.Substring(iSecondIDCommaIndex + 1, (iSecondSchoolCommaIndex - iSecondIDCommaIndex) - 1);        
        strSilverText += secondSchool;

        int iSecondTimeCommaIndex = strAnswer.IndexOf(",", iSecondSchoolCommaIndex + 1);
        string secondTime = strAnswer.Substring(iSecondSchoolCommaIndex + 1, (iSecondTimeCommaIndex - iSecondSchoolCommaIndex) - 1);        
        strSilverText += "\nTime: ";
        strSilverText += secondTime + " sec";
        silverText.text = strSilverText;


        string strBlonzText = "";
        // blonz
        int iThirdIDCommaIndex = strAnswer.IndexOf(",", iSecondTimeCommaIndex + 1);
        string thirdID = strAnswer.Substring(iSecondTimeCommaIndex + 1, (iThirdIDCommaIndex - iSecondTimeCommaIndex) - 1);
        Debug.Log("third ID: " + thirdID);
        strBlonzText += (thirdID + " / ");

        int iThirdSchoolCommaIndex = strAnswer.IndexOf(",", iThirdIDCommaIndex + 1);
        string ThirdSchool = strAnswer.Substring(iThirdIDCommaIndex + 1, (iThirdSchoolCommaIndex - iThirdIDCommaIndex) - 1);        
        strBlonzText += ThirdSchool;

        int iThirdTimeCommaIndex = strAnswer.IndexOf(",", iThirdSchoolCommaIndex + 1);
        string ThirdTime = strAnswer.Substring(iThirdSchoolCommaIndex + 1, (iThirdTimeCommaIndex - iThirdSchoolCommaIndex) - 1);        
        strBlonzText += "\nTime: ";
        strBlonzText += ThirdTime + " sec";
        blonzText.text = strBlonzText;

    }


    //================================================= BUTTON ============================================//
    public void BackButtonEvent()
    {
        SceneManager.LoadScene("SelectMap");
    }
}
