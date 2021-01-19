using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Addtext : MonoBehaviour
{

    GameObject message;
    GameObject prefab;
    LocationInfo iniLoca;
    request_text reqText;
    private int hukidashiNum;
    private const double lat2km = 110.987791 * 100; //緯度を距離に(日本北緯35度基準)
    private const double lon2km = 91.743119 * 100; //経度を距離に(日本北緯35度基準)

    public Vector3 ConvertGPS2km(double latitude, double longitude)
    {
        double dz = latitude * lat2km;   // -zが南方向
        double dx = longitude * lon2km; // +xが東方向
        return new Vector3((float)dx, 0, (float)dz);
    }

    public IEnumerator Make_hukidashi()
    {
        Debug.Log("Start coroutine");
        reqText = GameObject.Find("GameDirector").GetComponent<request_text>();
        
        yield return new WaitUntil(() => (GameObject.Find("AR Session Origin").GetComponent<ARcameraController>().iniCount == false));

        iniLoca = GameObject.Find("AR Session Origin").GetComponent<ARcameraController>().iniLocation;

        // ResourcesフォルダにあるNew SpriteプレハブをGameObject型で取得
        prefab = Resources.Load("hukidashi") as GameObject;
        hukidashiNum = reqText.textNum / 6;
        Debug.Log(hukidashiNum);
        for (int i = 0; i < hukidashiNum; i++)
        {
            hukidashi(i);
        }
    }

    private void hukidashi(int n)
    {
        double z = iniLoca.latitude - reqText.latitude[n];
        double x = iniLoca.longitude - reqText.longitude[n];

        // New Spriteプレハブを元に、インスタンスを生成、
        message = Instantiate(prefab, ConvertGPS2km(z, x), Quaternion.identity) as GameObject;
        message.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = reqText.letter[n];

        //デバッグ
/*
        if(n == 0)
        {
            GameObject.Find("hukidashi_x").GetComponent<Text>().text = message.transform.position.z.ToString();
            GameObject.Find("hukidashi_y").GetComponent<Text>().text = message.transform.position.x.ToString();
        }
*/
    }
}
