/**
 * 
 * 
 * 
 * 
 * 
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        

    }


    private void  CenterGui()
    {
        GameObject _radar = GameObject.Find("RadarFront");
        GameObject _dock = GameObject.Find("DockFront");
        GameObject _topca = GameObject.Find("TopCamera");
        //Camera _camtp = _topca.GetComponent<Camera>();
        Rect _rect = new Rect(20, Screen.height - 120, 130, 120);
        RectTransform _objrt = gameObject.GetComponent<RectTransform>();




        float _posx = _objrt.rect.width / 2 - 50;
        float _posy = _objrt.rect.height / 2 - 20;



        _radar.transform.localPosition = new Vector3(-_posx,
                                                        _posy - 30,
                                                        0);
        _dock.transform.localPosition = new Vector3(0,
                                                    -_posy,
                                                    0);

        //_camtp.pixelRect = _rect;

        Debug.Log(" GUI LOCAL SCALE " + transform.localScale + " DOCK LOCAL SCALE " + _dock.transform.localScale);
        Debug.Log(" GUI LOSSY SCALE " + transform.lossyScale + " DOCK LOSSY SCALE " + _dock.transform.lossyScale);

    }

    // Update is called once per frame
    void Update()
    {

        CenterGui();

    }
}
