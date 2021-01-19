using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ARcameraController : MonoBehaviour
{
    public LocationInfo iniLocation;
    public bool iniCount = true;
    private const double lat2km = 110.987791 * 100; //緯度を距離に(日本北緯35度基準)
    private const double lon2km = 91.743119 * 100; //経度を距離に(日本北緯35度基準)

    public Vector3 ConvertGPS2km(LocationInfo location)
    {
        double dz = (iniLocation.latitude - location.latitude) * lat2km;   // -zが南方向
        double dx = (iniLocation.longitude - location.longitude) * lon2km; // +xが東方向
        
        return new Vector3((float)dx, 0, (float)dz);
    }

    // Start is called before the first frame update
    void Start()
    {
        Input.location.Start();
        iniCount = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    public void UpdateCamera()
    {
        if (Input.location.isEnabledByUser)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                //MainSceneロード時一度だけ
                if (iniCount == true)
                {
                    iniLocation = Input.location.lastData;
                    iniCount = false;
                }

                LocationInfo location = Input.location.lastData;

                this.transform.position = ConvertGPS2km(location);
                Debug.Log(ConvertGPS2km(location));
            }
        }
    }
}
