using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textX : MonoBehaviour
{

    public Text targetText;
    public GameObject obj;
    const double lat2km = 111319.491;

    

    // Start is called before the first frame update
    void Start()
    {
        targetText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        double dx = obj.transform.position.x;// lat2km;
        targetText.text = dx.ToString();
    }
}
