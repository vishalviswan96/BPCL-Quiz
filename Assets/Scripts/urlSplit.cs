using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class urlSplit : MonoBehaviour
{
    public static urlSplit instance;

    private string[] split;
    private string[] splitX;
    private int index1;

    private string mach;
    private string bp;
    public string bpNumber, marks, time;
    public static string score;
    public string savedata;
    private string leaderboardUrl;

    private void Awake()
    {
        instance = this;    
    }


    void Start()
    {
        //score = 0.ToString();


        /*bpNumber = "469e922a39593a6978db67f878d0a864";
        marks = "10";
        time = "60";*/


        mach = Application.absoluteURL;

        urlfunc();
        Split();

    }

    void urlfunc()
    {
        index1 = mach.LastIndexOf("?");
        if (index1 > 0)
        {
            bp = mach.Substring(index1 + 1);
            Debug.Log(bp);
        }
    }

    void Split()
    {
        split = bp.Split('&');
        foreach (string x in split)
        {
            splitX = x.Split('=');
            if (splitX[0].Contains("s"))
            {
                bpNumber = splitX[1];
            }
            /*else if (splitX[0].Contains("name"))
            {
                name = splitX[1];
            }
            else if (splitX[0].Contains("region"))
            {
                region = splitX[1];
            }*/
            /*else if (splitX[0].Contains("score"))
            {
                score = splitX[1];
            }
            else if (splitX[0].Contains("gameid"))
            {
                gameID = splitX[1];
            }*/
            Debug.Log(splitX[1]);


        }

    }
    public void CallSaveData()
    {
        Debug.Log(marks);
        Debug.Log(time);
        savedata = "https://bpcl.eventsongo.com/app/umr1.php?s=" + bpNumber + "&m=" + marks + "&t=" + time;
        StartCoroutine(SaveData());
    }

    IEnumerator SaveData()
    {
        List<IMultipartFormSection> wwwform = new List<IMultipartFormSection>();
        /*wwwform.Add(new MultipartFormDataSection("s", bpNumber));
        wwwform.Add(new MultipartFormDataSection("m", marks));
        wwwform.Add(new MultipartFormDataSection("t", time));*/

        UnityWebRequest www = UnityWebRequest.Get(savedata);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Network Error : " + www.error);
        }
        else
        {
            Debug.Log("Return Server Text: " + www.downloadHandler.text);
            string str = www.downloadHandler.text;
            if (str == "1")
            {
                Debug.Log("Data Uploaded");
                //QuizOver();
            }
            else
            {
                Debug.LogError("Data Stoaring Error : " + www.error);
                Debug.Log("Data Upload failed");
            }
        }

    }

    public void QuizOver()
    {
        leaderboardUrl = "https://bpcl.eventsongo.com?s=" + bpNumber;
        Application.OpenURL(leaderboardUrl);
    }

}