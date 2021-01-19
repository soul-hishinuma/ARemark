using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class request_text : MonoBehaviour
{
    private const int LIST_NUM = 6;
    private string receiveText = "";
    public List<int> id = new List<int>();
    public List<string> name = new List<string>();
    public List<double> latitude = new List<double>();
    public List<double> longitude = new List<double>();
    public List<DateTime> datetime = new List<DateTime>();
    public List<string> letter = new List<string>();
    public int textNum;

    void Awake()
    {
        StartCoroutine(HttpConnect());
    }

    private IEnumerator HttpConnect()
    {
        string url = "http://172.30.224.57/request_text.php";

        //Unity2018~
        UnityWebRequest uwr = UnityWebRequest.Post(url, "");
        yield return uwr.SendWebRequest();

        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            receiveText = uwr.downloadHandler.text.ToString();
            Debug.Log(receiveText);
            string[] receiveTexts = receiveText.Split(',');
            textNum = receiveTexts.Length - 1;//最後にも,ついてるのでLength-1
            for (int i = 1; i < textNum; i++)
            {
                switch(i % LIST_NUM)
                {
                    case 0:
                        id.Add(int.Parse(receiveTexts[i]));
                        break;
                    case 1:
                        name.Add(receiveTexts[i].ToString());
                        break;
                    case 2:
                        latitude.Add(double.Parse(receiveTexts[i]));
                        break;
                    case 3:
                        longitude.Add(double.Parse(receiveTexts[i]));
                        break;
                    case 4:
                        datetime.Add(DateTime.Parse(receiveTexts[i]));
                        break;
                    case 5:
                        letter.Add(receiveTexts[i].ToString());
                        break;
                }
            }

            //GameObject.Find("GameDirector").GetComponent<Addtext>().Make_hukidashi();
            yield return StartCoroutine(GameObject.Find("GameDirector").GetComponent<Addtext>().Make_hukidashi());
            Debug.Log("Finished coroutine");
        }
    }
}
