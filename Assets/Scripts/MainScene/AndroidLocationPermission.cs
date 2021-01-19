using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidLocationPermission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
            Debug.Log("We have permission to access external storage!");
        else
            Debug.Log("Permission state: " + result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
