using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class post_text : MonoBehaviour
{
    public int id;
    public string name;
    public double latitude;
    public double longitude;
    public DateTime datetime;
    public string letter;

    void Start(){
        Input.location.Start();
    }

    public void SendSignal_Button_Push()
    {
        id = 0;//
        name = "";//

        if(Input.location.status == LocationServiceStatus.Running)
        {
            LocationInfo location = Input.location.lastData;
            latitude = location.latitude;
            longitude = location.longitude;
        }
        else
        {
            latitude = 0.0;
            longitude = 0.0;
        }

        datetime = DateTime.Now;

        letter = GameObject.Find("InputField").GetComponent<InputFieldManager>().letter;

        //送信開始
        StartCoroutine(HttpConnect());
    }

    private IEnumerator HttpConnect()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("name", name);
        form.AddField("latitude", latitude.ToString());
        form.AddField("longitude", longitude.ToString());
        form.AddField("datetime", datetime.ToString());
        form.AddField("letter", letter);

        string url = "http://172.30.224.57/post_text.php";
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
        }
    }
}
